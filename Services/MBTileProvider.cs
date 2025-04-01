using InsuranceWebApp.Data;
using Microsoft.Data.Sqlite;
namespace InsuranceWebApp.Services
{
    public class MBTileProvider
    {
        public RectangleBound? Bounds { get; private set; }
        public GeoPoint? Center { get; private set; }
        public string? Scheme { get; private set; }
        public int? MaskLevel { get; private set; }
        public int? MinZoom { get; private set; }
        public int? MaxZoom { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public string? MBTilesVersion { get; private set; }
        public string? ConnectionString { get; private set; }

        private const string MetadataQuery = "SELECT * FROM metadata;";
        private const string TilesQuery = "SELECT * FROM tiles WHERE zoom_level = @zoom AND tile_column = @x AND tile_row = @y;";
        private const string WALCommand = "PRAGMA journal_mode=WAL;";
        public MBTileProvider(string path)
        {
            ConnectionString = new SqliteConnectionStringBuilder
            {
                DataSource = path,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }.ToString();
            LoadMetadata();
        }
        private static void EnableWAL(SqliteConnection connection)
        {
            using var command = new SqliteCommand(WALCommand, connection);
            command.ExecuteNonQuery();
        }
        private void LoadMetadata()
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                EnableWAL(connection);
                using var command = new SqliteCommand(MetadataQuery, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["name"]?.ToString() ?? string.Empty;
                    var value = reader["value"];
                    switch (name.ToLower())
                    {
                        case "bounds":
                            var boundPosition = value?.ToString()?.Split(',') ?? ["0", "0"];
                            Bounds = new RectangleBound(Convert.ToDouble(boundPosition[0]), Convert.ToDouble(boundPosition[1]), Convert.ToDouble(boundPosition[2]), Convert.ToDouble(boundPosition[3]));
                            break;
                        case "center":
                            var centerPosition = value.ToString()?.Split(',') ?? ["0", "0"];
                            Center = new GeoPoint(Convert.ToDouble(centerPosition[0]), Convert.ToDouble(centerPosition[1]));
                            break;
                        case "scheme":
                            Scheme = value.ToString();
                            break;
                        case "masklevel":
                            MaskLevel = Convert.ToInt32(value);
                            break;
                        case "minzoom":
                            MinZoom = Convert.ToInt32(value);
                            break;
                        case "maxzoom":
                            MaxZoom = Convert.ToInt32(value);
                            break;
                        case "name":
                            Name = value.ToString();
                            break;
                        case "description":
                            Description = value.ToString();
                            break;
                        case "version":
                            MBTilesVersion = value.ToString();
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading metadata from MBTiles file", ex);
            }
        }
        public byte[]? GetTiles(int zoom, int x, int y)
        {
            if (x < 0
               || y < 0
               || zoom < MinZoom
               || zoom > 22
               || x >= Math.Pow(2, zoom)
               || y >= Math.Pow(2, zoom))
            {
                return null;
            }
            if (zoom > MaxZoom)
            {
                x = ScaleDown(x, zoom, (int)MaxZoom);
                y = ScaleDown(y, zoom, (int)MaxZoom);
                zoom = (int)MaxZoom;
            }
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                EnableWAL(connection);
                using var command = new SqliteCommand(TilesQuery, connection);
                if (Scheme == "tms")
                {
                    y = (int)(Math.Pow(2, zoom) - 1 - y);
                }
                command.Parameters.AddWithValue("@zoom", zoom);
                command.Parameters.AddWithValue("@x", x);
                command.Parameters.AddWithValue("@y", y);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return (byte[])reader["tile_data"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading tile from MBTiles file", ex);
            }
            if (MaskLevel != null && zoom > MaskLevel)
            {
                x = ScaleDown(x, zoom, (int)MaskLevel);
                y = ScaleDown(y, zoom, (int)MaskLevel);
                zoom = (int)MaskLevel;
                return GetTiles(zoom, x, y);
            }
            return null;
        }
        private static int ScaleDown(int x, int zoom, int mask)
        {
            return (int)Math.Floor(x / Math.Pow(2, zoom - mask));
        }
    }
}

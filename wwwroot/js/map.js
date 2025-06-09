let jsonLocationList = [];
const defaultLocation = [106.7035, 10.816];
function createLocationCard(item) {
  return /*html*/ `
        <li class="bg-white p-2 shadow text-gray-800 dark:text-neutral-200 dark:bg-neutral-800">
            <div class="flex flex-col text-sm space-y-2">
                <div><strong>Cơ sở y tế:</strong> ${item.hospitalName}</div>
                <div class="border-neutral-200"><strong>Địa chỉ:</strong> ${item.hospitalAddress}</div>
            </div>
        </li>`;
}

async function fetchNearByLocation(position, locationContainer) {
  const params = new URLSearchParams({
    latitude: position.latitude,
    longitude: position.longitude,
  });
  const headers = { "Content-Type": "application/json" };
  try {
    const response = await fetch(`${mapUrl}?${params.toString()}`, { headers });
    if (!response.ok) throw new Error("Network response was not ok");

    const data = await response.json();
    if (Array.isArray(data.hospitals)) {
      locationContainer.innerHTML = data.hospitals
        .map(createLocationCard)
        .join("");
      return data.hospitals;
    } else {
      return [];
    }
  } catch (error) {
    return [];
  }
}

function addLocationtoMap(locations, map) {
  locations.forEach((location) => {
    const marker = new maplibregl.Marker({ color: "red" })
      .setLngLat([location.longitude, location.latitude])
      .addTo(map);
    const popup = new maplibregl.Popup({ offset: 25 }).setText(
      location.hospitalName
    );
    marker.setPopup(popup);
  });
}

document.addEventListener("DOMContentLoaded", async () => {
  const locationContainer = document.getElementById("location-list");
  const map = new maplibregl.Map({
    container: "map",
    style:
      "https://api-uat-ibmi.baominh.vn:8500/insurance/static/map/style.json",
    center: defaultLocation,
    zoom: 14,
  });

  const geolocateControl = new maplibregl.GeolocateControl({
    positionOptions: { enableHighAccuracy: true, maximumAge: 0 },
    trackUserLocation: true,
  });

  map.addControl(
    new maplibregl.NavigationControl({
      visualizePitch: true,
      showZoom: true,
    })
  );
  map.addControl(geolocateControl);

  map.on("load", async () => {
    geolocateControl.trigger();

    map.fitBounds([
      [102.0409, 7.730748],
      [111.6685, 23.47731],
    ]);

    jsonLocationList = await fetchNearByLocation(
      { latitude: defaultLocation[1], longitude: defaultLocation[0] },
      locationContainer
    );

    geolocateControl.on("geolocate", async (position) => {
      const userLocation = position.coords;

      map.flyTo({
        center: [userLocation.longitude, userLocation.latitude],
        zoom: 20,
        essential: true,
      });

      jsonLocationList = await fetchNearByLocation(
        userLocation,
        locationContainer
      );
      addLocationtoMap(jsonLocationList, map);
    });
  });
});

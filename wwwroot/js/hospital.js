htmx.onLoad(function (content) {
  window.HSStaticMethods.autoInit();
});
let currentHospitalName = "";
let currentCityName = "";
let currentDistrictName = "";
let currentWardName = "";
let currentAdvancedSearch = false;
let currentPublicHospital = false;
let currentInPatient = false;
let currentOutPatient = false;
let currentDental = false;
document.addEventListener("DOMContentLoaded", () => {
  const accordation = document.getElementById("accordation");
  const inPatientCheckbox = document.getElementById("in-patient-checkbox");
  const outPatientCheckbox = document.getElementById("out-patient-checkbox");
  const dentalCheckbox = document.getElementById("dental-checkbox");
  const publicHospitalCheckbox = document.getElementById(
    "public-hospital-checkbox"
  );
  const hospitalSearch = document.getElementById("hospital-search");
  document.getElementById("toggle-btn").addEventListener("click", function () {
    accordation.style.display =
      accordation.style.display === "none" ? "inline-block" : "none";
    currentAdvancedSearch = !currentAdvancedSearch;
  });
  const citySelect = new TomSelect("#city-select", {
    searchField: "cityName",
    valueField: "cityName",
    load: function (query, callback) {
      var url = cityUrl + "?cityName=" + query;
      fetch(url)
        .then((response) => {
          if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
          }
          return response.json();
        })
        .then((json) => {
          callback(json);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    sortField: {
      field: "cityName",
      direction: "asc",
    },
    render: {
      option: function (data, escape) {
        return `<div>${escape(data.cityName)}</div>`;
      },
      item: function (item, escape) {
        return `<div>${escape(item.cityName)}</div>`;
      },
      no_results: function (data, escape) {
        return '<div class="no-results" style="text-size:15px">Không tìm thấy kết quả </div>';
      },
    },
  });

  const districtSelect = new TomSelect("#district-select", {
    searchField: "districtName",
    valueField: "districtName",
    load: function (query, callback) {
      var url = districtUrl + "?districtName=" + query;
      fetch(url)
        .then((response) => {
          if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
          }
          return response.json();
        })
        .then((json) => {
          callback(json);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    sortField: {
      field: "districtName",
      direction: "asc",
    },
    render: {
      option: function (data, escape) {
        return `<div>${escape(data.districtName)}</div>`;
      },
      item: function (item, escape) {
        return `<div>${escape(item.districtName)}</div>`;
      },
      no_results: function (data, escape) {
        return '<div class="no-results" style="font-size: 15px">Không tìm thấy kết quả </div>';
      },
    },
  });
  const wardSelect = new TomSelect("#ward-select", {
    searchField: "wardName",
    valueField: "wardName",
    load: function (query, callback) {
      var url = wardUrl + "?wardName=" + query;
      fetch(url)
        .then((response) => {
          if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
          }
          return response.json();
        })
        .then((json) => {
          callback(json);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    sortField: {
      field: "wardName",
      direction: "asc",
    },
    render: {
      option: function (data, escape) {
        return `<div>${escape(data.wardName)}</div>`;
      },
      item: function (item, escape) {
        return `<div>${escape(item.wardName)}</div>`;
      },
      no_results: function (data, escape) {
        return '<div class="no-results" style="font-size: 15px">Không tìm thấy kết quả </div>';
      },
    },
  });
  inPatientCheckbox.addEventListener("change", function () {
    currentInPatient = inPatientCheckbox.checked;
  });
  outPatientCheckbox.addEventListener("change", function () {
    currentOutPatient = outPatientCheckbox.checked;
  });
  dentalCheckbox.addEventListener("change", function () {
    currentDental = dentalCheckbox.checked;
  });
  publicHospitalCheckbox.addEventListener("change", function () {
    currentPublicHospital = publicHospitalCheckbox.checked;
  });
  document.getElementById("enter-btn").addEventListener("click", function () {
    currentCityName = citySelect.getValue();
    currentDistrictName = districtSelect.getValue();
    currentWardName = wardSelect.getValue();
    var hospitalName = hospitalSearch.value;
    let params = new URLSearchParams({
      hospitalName: hospitalName,
      cityName: currentCityName,
      districtName: currentDistrictName,
      wardName: currentWardName,
      IsAdvancedSearch: currentAdvancedSearch,
      IsPublicHospital: currentPublicHospital,
      InPatient: currentInPatient,
      OutPatient: currentOutPatient,
      Dental: currentDental,
    });
    const url = `${hospitalUrl}?${params.toString()}`;
    htmx.ajax("GET", url, {
      target: "#hospital-table",
    });
  });
  document.getElementById("reset-btn").addEventListener("click", function () {
    citySelect.clear(false);
    districtSelect.clear(false);
    wardSelect.clear(false);
    inPatientCheckbox.checked = false;
    outPatientCheckbox.checked = false;
    dentalCheckbox.checked = false;
    publicHospitalCheckbox.checked = false;
  });
});

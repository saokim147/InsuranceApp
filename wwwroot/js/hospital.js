htmx.onLoad(() => {
  window.HSStaticMethods.autoInit();
});

//htmx.logger = function (elt, event, data) {
//  if (console) {
//    console.log(event, elt, data);
//  }
//};

var isCreatedOpen = false;



// get the item id
window.getSessionIdList = function () {
  let items = [];
  for (let i = 0; i < sessionStorage.length; i++) {
    let key = sessionStorage.key(i);
    if (key.startsWith("delete-checkbox")) {
      let item = sessionStorage.getItem(key);
      if (item) {
        items.push(item);
      }
    }
  }
  return items;
};

window.getSessionIndexList = function () {
  let items = [];
  for (let i = 0; i < sessionStorage.length; i++) {
    let key = sessionStorage.key(i);
    if (key.startsWith("delete-checkbox")) {
      items.push(key);
    }
  }
  return items;
};

window.restoreCheckedCheckbox = function () {
  let items = getSessionIndexList();
  for (let item of items) {
    let checkbox = document.getElementById(item);
    if (checkbox) {
      checkbox.checked = true;
    }
  }
};

window.addEventListener("unload", () => {
  sessionStorage.clear();
});

const successEditHandler = () => {
  window.dispatchEvent(
    new CustomEvent("notify", {
      detail: {
        variant: "success",
        title: "Success!",
        message: "Edit successfully",
      },
    })
  );
  HSOverlay.close("#editFormContainer");
};
const failedEditHandler = () => {
  window.dispatchEvent(
    new CustomEvent("notify", {
      detail: { variant: "danger", title: "Error!", message: "Edit failed" },
    })
  );
};

const successCreateHandler = () => {
  window.dispatchEvent(
    new CustomEvent("notify", {
      detail: {
        variant: "success",
        title: "Success!",
        message: "Create successfully",
      },
    })
  );
  HSOverlay.close('#addFormContainer');
};

const failedCreateHandler = () => {
  window.dispatchEvent(
    new CustomEvent("notify", {
      detail: { variant: "danger", title: "Error!", message: "Create failed" },
    })
  );
};

document.addEventListener("htmx:afterSwap", (e) => {
  if (e.detail.elt.id === "editFormContainer") {
    new AutoCompleteHandler("edit-citySelect","edit-city-list","CityId");
    new AutoCompleteHandler("edit-districtSelect","edit-district-list","DistrictId");
    new AutoCompleteHandler("edit-wardSelect","edit-ward-list","WardId");
    document.getElementById("edit-citySelect").value = currentCityName;
    document.getElementById("edit-districtSelect").value = currentDistrictName;
    document.getElementById("edit-wardSelect").value = currentWardName;
    document.addEventListener("successUpdateEvent", successEditHandler);  
    document.addEventListener("failedUpdateEvent", failedEditHandler);
  }
});

document.addEventListener("DOMContentLoaded", () => {
    new AutoCompleteHandler("create-citySelect","create-city-list","CityId");
    new AutoCompleteHandler("create-districtSelect","create-district-list","DistrictId");
    new AutoCompleteHandler("create-wardSelect","create-ward-list","WardId");
    document.addEventListener("successCreateEvent", successCreateHandler);
    document.addEventListener("failedCreateEvent", failedCreateHandler);
  // accordation
  document.getElementById("toggle-btn").addEventListener("click", () => {
    let accordation = document.getElementById("accordation");
    accordation.style.display =
      accordation.style.display === "none" ? "inline-block" : "none";
  });

  document.getElementById("reset-btn").addEventListener("click", () => {
    document.getElementById("in-patient-checkbox").checked = false;
    document.getElementById("out-patient-checkbox").checked = false;
    document.getElementById("dental-checkbox").checked = false;
    document.getElementById("public-hospital-checkbox").checked = false;
    document.getElementById("citySelect").value = "";
    document.getElementById("districtSelect").value = "";
    document.getElementById("wardSelect").value = "";
  });
});

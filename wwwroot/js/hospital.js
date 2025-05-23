htmx.onLoad(() => {
  window.HSStaticMethods.autoInit();
});

//htmx.logger = function (elt, event, data) {
//  if (console) {
//    console.log(event, elt, data);
//  }
//};

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

window.addEventListener("unload", () => sessionStorage.clear());

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
  HSOverlay.close("#addFormContainer");
};

const failedCreateHandler = () => {
  window.dispatchEvent(
    new CustomEvent("notify", {
      detail: { variant: "danger", title: "Error!", message: "Create failed" },
    })
  );
};

const $id = document.getElementById.bind(document);

document.addEventListener("htmx:afterSwap", (e) => {
    if (e.detail.elt.id === "editFormContainer") {
        new AutoCompleteHandler("edit-citySelect", "edit-city-list", "CityId");
        new AutoCompleteHandler("edit-districtSelect", "edit-district-list", "DistrictId");
        new AutoCompleteHandler("edit-wardSelect", "edit-ward-list", "WardId");

        $id("edit-citySelect").value = currentCityName;
        $id("edit-districtSelect").value = currentDistrictName;
        $id("edit-wardSelect").value = currentWardName;

        document.addEventListener("successUpdateEvent", successEditHandler);
        document.addEventListener("failedUpdateEvent", failedEditHandler);
    }
});

document.addEventListener("DOMContentLoaded", () => {
    new AutoCompleteHandler("create-citySelect", "create-city-list", "CityId");
    new AutoCompleteHandler("create-districtSelect", "create-district-list", "DistrictId");
    new AutoCompleteHandler("create-wardSelect", "create-ward-list", "WardId");

    document.addEventListener("successCreateEvent", successCreateHandler);
    document.addEventListener("failedCreateEvent", failedCreateHandler);

    $id("toggle-btn").addEventListener("click", () => {
        let accordation = $id("accordation");
        accordation.style.display = accordation.style.display === "none" ? "inline-block" : "none";
    });

    $id("reset-btn").addEventListener("click", () => {
        $id("in-patient-checkbox").checked = false;
        $id("out-patient-checkbox").checked = false;
        $id("dental-checkbox").checked = false;
        $id("public-hospital-checkbox").checked = false;
        $id("citySelect").value = "";
        $id("districtSelect").value = "";
        $id("wardSelect").value = "";
    });
});

function toggleHiddenRow(index) {
  const rowLink = document.getElementById(`hidden-row-${index}`);
  rowLink.style.display =
    rowLink.style.display === "none" ? "table-cell" : "none";
}

function toggleCardDetail(index) {
  const detailElement = document.getElementById(`card-detail-${index}`);
  detailElement.classList.toggle("hidden");
}

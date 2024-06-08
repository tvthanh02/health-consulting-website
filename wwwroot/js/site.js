// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// handle toggle navbar

window.addEventListener("DOMContentLoaded", () => {
  const barEle = document.querySelector(".header__bar");

  if (barEle) {
    barEle.addEventListener("click", () => {
      document.querySelector(".nav").classList.toggle("open");
    });
  }
});

// handle display content more service detail

const serviceDetailIntroduceEle = document.querySelector(
  ".service-detail__content-introduce"
);
const btnMoreContent = document.querySelector(
  ".service-detail__content-introduce-action"
);

if (serviceDetailIntroduceEle) {
  btnMoreContent.addEventListener("click", () => {
    serviceDetailIntroduceEle.classList.toggle("toggle");
    if (btnMoreContent.innerText == "Hiện thêm") {
      btnMoreContent.innerText = "ẩn bớt";
      return;
    }
    btnMoreContent.innerText = "Hiện thêm";
  });
}

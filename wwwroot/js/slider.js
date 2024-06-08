const $$ = (selector) => document.querySelector(selector);
const $$$ = (selector) => document.querySelectorAll(selector);

const handleSetupSlide = (sliderSelector, desktop, tablet, mobile, time) => {
  //state global
  let isDragging = false,
    startX,
    scrollLeftCurent,
    timerId;

  //elements
  const slider = $$(sliderSelector);
  const slides = slider.querySelector(".slides");
  const btnActions = slider.querySelectorAll(".btn-action");

  //size
  const slideItemEles = slides.querySelectorAll(".slide-item");
  let slideItemWidth = slideItemEles[0].offsetWidth;
  let itemPerView = Math.floor(slides.offsetWidth / slideItemWidth);

  // pre handle infinity scroll
  const liEleChildSlider = [...slides.children];

  const beforeMoreForInfinity = liEleChildSlider.slice(-itemPerView).reverse();
  const afterMoreForInfinity = liEleChildSlider.slice(0, itemPerView);

  beforeMoreForInfinity.forEach((item) =>
    slides.insertAdjacentHTML("afterbegin", item.outerHTML)
  );

  afterMoreForInfinity.forEach((item) =>
    slides.insertAdjacentHTML("beforeend", item.outerHTML)
  );

  btnActions.forEach((btn) => {
    btn.addEventListener("click", function () {
      slides.scrollLeft += this.id == "left" ? -slideItemWidth : slideItemWidth;
    });
  });

  // handle infinity scroll
  const handleInfinityScroll = () => {
    if (slides.scrollLeft <= 0) {
      slides.classList.add("no-transition");
      slides.scrollLeft = slides.scrollWidth - 2 * slides.offsetWidth;
      slides.classList.remove("no-transition");
    } else if (slides.scrollLeft === slides.scrollWidth - slides.offsetWidth) {
      slides.classList.add("no-transition");
      slides.scrollLeft = slides.offsetWidth;
      slides.classList.remove("no-transition");
    }

    clearTimeout(timerId);
    if (!slider.matches(":hover")) autoPlay();
  };

  const autoPlay = () => {
    if (window.innerWidth < 479) return;
    timerId = setTimeout(() => {
      slides.scrollLeft += slideItemWidth;
    }, time);
  };

  autoPlay();

  const handleEditStyleSlide = () => {
    slides.style.gridAutoColumns = `calc(100% / ${desktop})`;

    const mediaQuery = window.matchMedia(
      "(min-width: 768px) and (max-width: 1023px)"
    );

    const mediaQuery2 = window.matchMedia("(max-width: 767px)");

    if (mediaQuery.matches) {
      slides.style.gridAutoColumns = `calc(100% / ${tablet})`;
    }

    if (mediaQuery2.matches) {
      slides.style.gridAutoColumns = `calc(100% / ${mobile})`;
    }

    mediaQuery.addListener(() => {
      if (mediaQuery.matches) {
        slides.style.gridAutoColumns = `calc(100% / ${tablet})`;
      }
    });

    mediaQuery2.addListener(() => {
      if (mediaQuery2.matches) {
        slides.style.gridAutoColumns = `calc(100% / ${mobile})`;
      }
    });

    slideItemWidth = slideItemEles[0].offsetWidth;
    itemPerView = Math.floor(slides.offsetWidth / slideItemWidth);
  };

  handleEditStyleSlide();

  const handleMouseDown = (e) => {
    isDragging = true;
    slides.classList.add("dragging");
    startX = e.pageX;
    scrollLeftCurent = slides.scrollLeft;
  };

  const handleMouseMove = (e) => {
    if (!isDragging) return;
    slides.scrollLeft = startX - e.pageX + scrollLeftCurent;
  };

  const handleMouseUp = () => {
    isDragging = false;
    slides.classList.remove("dragging");
  };

  slides.addEventListener("scroll", handleInfinityScroll);
  slides.addEventListener("mousedown", handleMouseDown);
  slides.addEventListener("mousemove", handleMouseMove);
  document.addEventListener("mouseup", handleMouseUp);
  slider.addEventListener("mouseenter", () => clearTimeout(timerId));
  slider.addEventListener("mouseleave", () => {
    autoPlay();
  });
};

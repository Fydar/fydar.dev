const clamp = (a, min = 0, max = 1) => Math.min(max, Math.max(min, a));
const invlerp = (x, y, a) => clamp((a - x) / (y - x));

function UpdateRelativeElements() {
  var fsaRelativeObjects = document.getElementsByClassName("fsa-relative");

  for (let i = 0; i < fsaRelativeObjects.length; i++) {
    var fsaRelative = fsaRelativeObjects[i];

    var bounds = fsaRelative.getBoundingClientRect();
    var scroll = invlerp(window.innerHeight, -bounds.height, bounds.top);

    fsaRelative.style.setProperty("--animation-time", scroll);
  }
}

function UpdateScreenPerspective() {
  var screenPerspectives = document.getElementsByClassName("scene-screen-perspective");

  for (let i = 0; i < screenPerspectives.length; i++) {
    var screenPerspective = screenPerspectives[i];

    var bounds = screenPerspective.getBoundingClientRect();
    var scroll = invlerp(-bounds.height, window.innerHeight, bounds.top);

    screenPerspective.style.perspectiveOrigin = "50% " + (((window.innerHeight / bounds.height) * ((scroll * 2) - 1) * -100) + 50) + "%";
    screenPerspective.style.perspective = (1024 / window.devicePixelRatio) + "px";
  }
}

window.addEventListener("scroll",
  () => {
    UpdateRelativeElements();
    UpdateScreenPerspective();
  }, false
);
window.addEventListener("resize",
  () => {
    UpdateRelativeElements();
    UpdateScreenPerspective();
  }, false
);
UpdateRelativeElements();
UpdateScreenPerspective();

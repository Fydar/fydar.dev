const clamp = (a, min = 0, max = 1) => Math.min(max, Math.max(min, a));
const invlerp = (x, y, a) => clamp((a - x) / (y - x));

function UpdateRelativeElements() {
	var fsaRelativeObjects = document.getElementsByClassName("fsa-relative");

	for (let p = 0; p < fsaRelativeObjects.length; p++) {
		var fsaRelative = fsaRelativeObjects[p];

		var bounds = fsaRelative.getBoundingClientRect();
		var scroll = invlerp(-bounds.height, screen.height, bounds.top);

		fsaRelative.style.setProperty("--scroll", scroll);
	}
}

window.addEventListener("scroll",
	() => { UpdateRelativeElements(); }, false
);
window.addEventListener("resize",
	() => { UpdateRelativeElements(); }, false
);
UpdateRelativeElements();

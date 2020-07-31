const clamp = (a, min = 0, max = 1) => Math.min(max, Math.max(min, a));
const invlerp = (x, y, a) => clamp((a - x) / (y - x));
const lerp = function (value1, value2, amount) {
	amount = amount < 0 ? 0 : amount;
	amount = amount > 1 ? 1 : amount;
	return value1 + (value2 - value1) * amount;
};

window.addEventListener("scroll", updateParallax);
window.addEventListener("resize", updateParallax);
updateParallax();

(function () {
	updateParallax();
})();

function updateParallax() {
	var paralaxContainers = document.getElementsByClassName("parallax-container");

	for (let p = 0; p < paralaxContainers.length; p++) {
		var paralaxContainer = paralaxContainers[p];

		var offset = 0;
		if (paralaxContainer.classList.contains("parallax-anchor-top")) {
			offset = screen.height * 0.25;
		}

		var bounds = paralaxContainer.getBoundingClientRect();
		var scroll = invlerp(-bounds.height, screen.height, bounds.top + offset);

		for (let i = 0; i < paralaxContainer.children.length; i++) {
			var layer = paralaxContainer.children[i];

			var layerBound = layer.getBoundingClientRect();

			var offset = lerp(0, -layerBound.height + bounds.height, scroll);

			layer.style.top = offset + "px";
		}
	}
}


queueMicrotask(console.log.bind(console, "%c   ___             __\n /\'___\\           /\\ \\\n/\\ \\__/  __  __   \\_\\ \\     __     _ __\n\\ \\ ,__\\/\\ \\/\\ \\  /\'_` \\  /\'__`\\  /\\`\'__\\\n \\ \\ \\_/\\ \\ \\_\\ \\/\\ \\L\\ \\/\\ \\L\\.\\_\\ \\ \\/\n  \\ \\_\\  \\/`____ \\ \\___,_\\ \\__/.\\_\\\\ \\_\\\n   \\/_/   `/___/> \\/__,_ /\\/__/\\/_/ \\/_/\n             /\\___/\n             \\/__/", "font-family: monospace; white-space: nowrap"));
queueMicrotask(console.log.bind(console, "This website has been made using ASP.NET Core and Blazor.\nLike what you see? I\'m available for hire!\n\nhttps://fydar.dev/contact"));

const clamp = (a, min = 0, max = 1) => Math.min(max, Math.max(min, a));
const invlerp = (x, y, a) => (a - x) / (y - x);
function lerp(start, end, amt) {
    return (1 - amt) * start + amt * end
}

function UpdateRelativeElements() {
    var elements = document.getElementsByClassName("parallax-focalanchortop");
    for (let i = 0; i < elements.length; i++) {
        var element = elements[i];
        var clientRect = element.getBoundingClientRect()
        var time = invlerp(window.innerHeight, -clientRect.height, clientRect.top);
        element.style.setProperty("--animation-time", time.toFixed(5));
        element.style.setProperty("--parallax-offset", lerp(-clientRect.height, clientRect.height, time).toFixed(1));
    }
}

UpdateRelativeElements();

window.addEventListener("scroll",
    eventArgs => {
        UpdateRelativeElements();
    }, { passive: true }
);

window.addEventListener("resize",
    eventArgs => {
        UpdateRelativeElements();
    }, { passive: true }
);

var lastX = 0;
var lastY = 0;

window.addEventListener("pointermove",
    eventArgs => {
        lastX = eventArgs.clientX;
        lastY = eventArgs.clientY;
        document.documentElement.style.setProperty("--pointer-fixed", lastX.toFixed(2) + "px " + lastY.toFixed(2) + "px");
        UpdateRelativeElements();
    }, { passive: true }
);

window.document.addEventListener("pointerleave",
    eventArgs => {
        var pointerRelativeElements = document.getElementsByClassName("pointer-relative");

        for (let i = 0; i < pointerRelativeElements.length; i++) {
            var pointerRelativeElement = pointerRelativeElements[i];
            pointerRelativeElement.classList.add("pointer-none");
        }
    }, { passive: true }
);

window.document.addEventListener("pointerenter",
    eventArgs => {
        var pointerRelativeElements = document.getElementsByClassName("pointer-relative");

        for (let i = 0; i < pointerRelativeElements.length; i++) {
            var pointerRelativeElement = pointerRelativeElements[i];
            pointerRelativeElement.classList.remove("pointer-none");
        }
    }, { passive: true }
);

function NavHighlighter() {
    // Get all sections that have an ID defined
    let sections = document.querySelectorAll("h2[id]");

    // Get current scroll position
    let scrollY = window.scrollY;

    // Now we loop through sections to get height, top and ID values for each
    sections.forEach(current => {
        const wrapper = current.parentElement;
        const sectionHeight = wrapper.offsetHeight;

        const sectionTop = (wrapper.getBoundingClientRect().top + scrollY) - 50 - (window.innerHeight * 0.5 * getScrollPercent());
        sectionId = current.getAttribute("id");

        if (
            scrollY > sectionTop &&
            scrollY <= sectionTop + sectionHeight
        ) {
            document.querySelector("ol li a[href*=\"" + sectionId + "\"]").classList.add("active");
        } else {
            document.querySelector("ol li a[href*=\"" + sectionId + "\"]").classList.remove("active");
        }
    });
}
function getScrollPercent() {
    var h = document.documentElement,
        b = document.body,
        st = 'scrollTop',
        sh = 'scrollHeight';
    return (h[st] || b[st]) / ((h[sh] || b[sh]) - h.clientHeight);
}

NavHighlighter();

window.addEventListener("scroll",
    eventArgs => {
        NavHighlighter();
    }, { passive: true }
);

window.addEventListener("resize",
    eventArgs => {
        NavHighlighter();
    }, { passive: true }
);

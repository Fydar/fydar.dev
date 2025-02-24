
queueMicrotask(console.log.bind(console, "%c   ___             __\n /\'___\\           /\\ \\\n/\\ \\__/  __  __   \\_\\ \\     __     _ __\n\\ \\ ,__\\/\\ \\/\\ \\  /\'_` \\  /\'__`\\  /\\`\'__\\\n \\ \\ \\_/\\ \\ \\_\\ \\/\\ \\L\\ \\/\\ \\L\\.\\_\\ \\ \\/\n  \\ \\_\\  \\/`____ \\ \\___,_\\ \\__/.\\_\\\\ \\_\\\n   \\/_/   `/___/> \\/__,_ /\\/__/\\/_/ \\/_/\n             /\\___/\n             \\/__/", "font-family: monospace; white-space: nowrap"));
queueMicrotask(console.log.bind(console, "This website has been made using ASP.NET Core and Blazor.\nLike what you see? I\'m available for hire!\n\nhttps://fydar.dev/contact"));

const clamp = (a, min = 0, max = 1) => Math.min(max, Math.max(min, a));
const invlerp = (x, y, a) => (a - x) / (y - x);
function lerp(start, end, amt) {
    return (1 - amt) * start + amt * end
}

function UpdateParallaxLayerSize() {
    var elements = document.getElementsByClassName("parallax-layer");
    for (let i = 0; i < elements.length; i++) {
        var element = elements[i];

        var containerRect = element.parentElement.parentElement.getBoundingClientRect();
        element.style.width = containerRect.width + "px";
        element.style.height = containerRect.height + "px";
    }
}
function UpdateRelativeElements() {
    var elements = document.getElementsByClassName("parallax-focalanchortop");
    for (let i = 0; i < elements.length; i++) {
        var element = elements[i];
        var clientRect = element.getBoundingClientRect()
        var time = clamp(invlerp(clientRect.height, -clientRect.height, clientRect.top), 0, 1);
        element.style.setProperty("--animation-time", time.toFixed(5));
        element.style.setProperty("--parallax-offset", lerp(-clientRect.height, clientRect.height, time).toFixed(1));
    }
}

UpdateParallaxLayerSize();
UpdateRelativeElements();

window.addEventListener("scroll",
    eventArgs => {
        UpdateRelativeElements();
    }, { passive: true }
);

window.addEventListener("resize",
    eventArgs => {
        UpdateParallaxLayerSize();
        UpdateRelativeElements();
    }, { passive: true }
);



function createStylesheet() {
    const style = document.createElement('style');
    document.head.appendChild(style);
    return style.sheet;
}

function addCSSRule(stylesheet, selector, styles) {
    if (stylesheet.insertRule) { // Modern browsers
        stylesheet.insertRule(`${selector} { ${styles} }`, stylesheet.cssRules.length);
    } else if (stylesheet.addRule) { // Older IE (rarely needed now)
        stylesheet.addRule(selector, styles, -1);
    } else {
        console.error("Adding CSS rule not supported.");
    }
}

function findCSSRule(stylesheet, selector) {
    const rules = stylesheet.cssRules || stylesheet.rules;
    for (let i = 0; i < rules.length; i++) {
        const rule = rules[i];
        if (rule instanceof CSSStyleRule && rule.selectorText === selector) {
            return rule;
        }
    }
    return null;
}

var proceduralStylesheet = createStylesheet();
addCSSRule(proceduralStylesheet, ".pointer-relative", "--pointer-fixed: 0px 0px;");
const rule = findCSSRule(proceduralStylesheet, ".pointer-relative");

var lastX = 0;
var lastY = 0;

window.addEventListener("pointermove",
    eventArgs => {
        lastX = eventArgs.clientX;
        lastY = eventArgs.clientY;

        rule.style.setProperty("--pointer-fixed", lastX.toFixed(2) + "px " + lastY.toFixed(2) + "px");
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
    let sections = document.querySelectorAll(".heading-wrapper[id]");

    // Get current scroll position
    let scrollY = window.scrollY;

    // Now we loop through sections to get height, top and ID values for each
    sections.forEach(current => {
        const sectionHeight = current.offsetHeight;

        const sectionTop = (current.getBoundingClientRect().top + scrollY) - 50 - (window.innerHeight * 0.5 * getScrollPercent());
        sectionId = current.getAttribute("id");

        var queryResults = document.querySelector("ol li a[href*=\"" + sectionId + "\"]");

        if (queryResults != null) {
            if (scrollY > sectionTop && scrollY <= sectionTop + sectionHeight) {
                queryResults.classList.add("active");
            } else {
                queryResults.classList.remove("active");
            }
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

function GraphScaler() {
    var elements = document.getElementsByClassName("graph");
    for (let i = 0; i < elements.length; i++) {
        var element = elements[i];
        var clientRect = element.getBoundingClientRect()

        var targetSize = window.getComputedStyle(element).getPropertyValue("--Lodestone-graph-targetsize");
        element.style.setProperty("--Lodestone-graph-scale", clientRect.width / targetSize);
    }
}

NavHighlighter();
GraphScaler();

window.addEventListener("scroll",
    eventArgs => {
        NavHighlighter();
    }, { passive: true }
);

window.addEventListener("resize",
    eventArgs => {
        NavHighlighter();
        GraphScaler();
    }, { passive: true }
);


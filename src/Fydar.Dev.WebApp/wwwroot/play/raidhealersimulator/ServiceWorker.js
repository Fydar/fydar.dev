const cacheName = "fydar-raidhealersimulator-v0.1.2";
const contentToCache = [
    "./",
    "build/raidhealersimulator.loader.js",
    "build/raidhealersimulator.framework.js.br",
    "build/raidhealersimulator.data.br",
    "build/raidhealersimulator.wasm.br"
];

self.addEventListener('install', function (e) {
    e.waitUntil((async function () {
        const cache = await caches.open(cacheName);
        await cache.addAll(contentToCache);
    })());
});

const putInCache = async (request, response) => {
    const cache = await caches.open(cacheName);
    await cache.put(request, response);
};

const cacheFirst = async ({ request }) => {
    const responseFromCache = await caches.match(request);
    if (responseFromCache) {
        return responseFromCache;
    }

    try {
        const responseFromNetwork = await fetch(request);
        putInCache(request, responseFromNetwork.clone());
        return responseFromNetwork;
    } catch (error) {
        return new Response("Network error happened", {
            status: 408,
            headers: { "Content-Type": "text/plain" },
        });
    }
};

self.addEventListener("fetch", (event) => {
    event.respondWith(
        cacheFirst({
            request: event.request
        }),
    );
});

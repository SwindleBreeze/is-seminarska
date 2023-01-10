self.addEventListener('install', function(event) {
    // Perform install steps
    event.waitUntil(
      caches.open('sloveniatrips-cache')
        .then(function(cache) {
          console.log('Opened cache');
          return cache.addAll([
            '/',
          ]);
        })
    );
  });
  
  self.addEventListener('fetch', function(event) {
    event.respondWith(
      caches.match(event.request)
        .then(function(response) {
          // Cache hit - return response
          if (response) {
            return response;
          }
          return fetch(event.request);
        }
      )
    );
  });
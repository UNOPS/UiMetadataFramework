!function(e){function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}var t={};n.m=e,n.c=t,n.i=function(e){return e},n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{configurable:!1,enumerable:!0,get:r})},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,n){return Object.prototype.hasOwnProperty.call(e,n)},n.p="",n(n.s=3)}([function(e,n,t){"use strict";Object.defineProperty(n,"__esModule",{value:!0});var r=t(2);Object.keys(r).forEach(function(e){"default"!==e&&"__esModule"!==e&&Object.defineProperty(n,e,{enumerable:!0,get:function(){return r[e]}})})},function(e,n,t){"use strict";function r(e,n){if(!(e instanceof n))throw new TypeError("Cannot call a class as a function")}Object.defineProperty(n,"__esModule",{value:!0});var o=function(){function e(e,n){for(var t=0;t<n.length;t++){var r=n[t];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(n,t,r){return t&&e(n.prototype,t),r&&e(n,r),n}}();n.$=function(){function e(){r(this,e)}return o(e,null,[{key:"get",value:function(e){return new Promise(function(n,t){var r=new XMLHttpRequest;r.open("GET",e),r.onload=function(){200===r.status?n(r.response):t(Error(r.statusText))},r.onerror=function(){t(Error("Network Error"))},r.send()})}}]),e}()},function(e,n,t){"use strict";function r(e,n){if(!(e instanceof n))throw new TypeError("Cannot call a class as a function")}Object.defineProperty(n,"__esModule",{value:!0}),n.UmfApp=void 0;var o=function(){function e(e,n){for(var t=0;t<n.length;t++){var r=n[t];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}return function(n,t,r){return t&&e(n.prototype,t),r&&e(n,r),n}}(),u=t(1);n.UmfApp=function(){function e(){r(this,e)}return o(e,[{key:"getMetadata",value:function(e){return u.$.get("/form/metadata/"+e).then(function(e){return console.log(e),e})}},{key:"getAllMetadata",value:function(){return null}}]),e}()},function(e,n,t){"use strict";var r=t(0),o=function(e){if(e&&e.__esModule)return e;var n={};if(null!=e)for(var t in e)Object.prototype.hasOwnProperty.call(e,t)&&(n[t]=e[t]);return n.default=e,n}(r);window.umf=o}]);
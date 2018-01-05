import Vue from 'vue';

const EventBus = new Vue();

Object.defineProperties(Vue.prototype, {
	$bus: {
		get: function () {
			return EventBus;
		}
	}
});

export default EventBus;
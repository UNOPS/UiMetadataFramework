import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router';
import App from './components/App.vue'

const viewNames = ['completed', 'active', '*'];
const routes = viewNames.map((view): RouteConfig => ({
    path: '/Main',
    component: App,
    props: {
        currentView: view === '*' ? 'all' : view,
    },
}));

const router = new VueRouter({
    routes,
});

Vue.use(VueRouter);

new Vue({
  el: '#app',
  router,
  render: h => h(App)
})
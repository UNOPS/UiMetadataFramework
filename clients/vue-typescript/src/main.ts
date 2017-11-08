import Vue from 'vue';
import AppRouter from './core/AppRouter';
import './sass/main.scss';
import { NavbarComponent } from 'components/navbar';

new Vue({
    el: '#app-main',
    components: { 'navbar': NavbarComponent },
    router: AppRouter
}).$mount('#app-main')//mount the router on the app;
import Vue from 'vue';
import Component from 'vue-class-component';

import './home.scss';

@Component({
    template: require('./home.html')
})
export class HomeComponent extends Vue {

    package: string = this.$root.$data.title;
    repo: string = 'https://github.com/mohammed-fuad/uimf-vue-typescript';
    mode: string = process.env.ENV;
}

import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import { Link } from './link';
import { Logger } from '../../util/log';

@Component({
    template: require('./navbar.html')
})
export class NavbarComponent extends Vue {

    protected logger: Logger;

    inverted: boolean = true; // default value

    object: { default: string } = { default: 'Default object property!' }; // objects as default values don't need to be wrapped into functions

    private self;
    links: Link[] = [];

    @Watch('$route.path')
    pathChanged() {
        // this.logger.info('Changed current path to: ' + this.$route.path);
    }

    mounted() {
        // if (!this.logger) this.logger = new Logger();
        // this.$nextTick(() => this.logger.info(this.object.default));
        this.links.push(new Link('Home', '/#/'));
        this.links.push(new Link('About', '/#/about'));

        this.$nextTick(() => {
            var app = this.$root.$data._app;

            for (let form of app.forms) {
                if (form.customProperties != null && form.label) {
                    this.links.push(new Link(form.label, app.makeUrl(form.id, null)))
                }
            }
        });
    }
}

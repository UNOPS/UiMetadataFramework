<template>
  <div id="app">
    <main>
      <img src="../assets/logo.png" alt="Vue.js PWA">
    </main>
  </div>
</template>

<script lang="ts">
import Vue from 'vue'
import Component from 'vue-class-component'
import { Prop } from 'vue-property-decorator'
import { State, Action } from 'vuex-class'
import { UmfServer, UmfApp } from "core-framework";
import * as handlers from "core-handlers";
import controlRegister from "../ControlRegister";

@Component({})
export default class extends Vue {
		protected server: UmfServer;

	mounted() {
		if (!this.server) {
			this.server = new UmfServer("http://localhost:62790/api/form/metadata", "http://localhost:62790/api/form/run")
		};

		var app = new UmfApp(this.server, controlRegister);
		  app.load().then(response => {
		    app.registerResponseHandler(new handlers.MessageResponseHandler());
		    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
		  app.go(form, inputFieldValues);
		})); });
	}
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #35495E;
}
</style>

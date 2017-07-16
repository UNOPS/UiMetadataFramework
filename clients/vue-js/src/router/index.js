import Vue from 'vue'
import Router from 'vue-router'
import Hello from '@/components/Hello'
import Help from '@/components/Help'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/Help',
      name: 'Help',
      component: Help
    }
  ]
})

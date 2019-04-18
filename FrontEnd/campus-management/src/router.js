import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Register from "./views/Register.vue";
import News from "./views/News.vue";
import AccommodationRequest from "./views/AccommodationRequest.vue";
import NewsCreator from "./views/NewsCreator.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/about",
      name: "about",
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () =>
        import(/* webpackChunkName: "about" */ "./views/About.vue")
    },
    {
      path: "/camine",
      name: "camine",
      component: Home
    },
    {
      path: "/utile",
      name: "utile",
      component: Home
    },
    {
      path: "/cantina",
      name: "cantina",
      component: Home
    },
    {
      path: "/contact",
      name: "cantact",
      component: () =>
        import(/* webpackChunkName: "about" */ "./views/About.vue")
    },
    {
      path: "/register",
      name: "regiser",
      component: Register
    },
    {
      path: "/news",
      name: "news",
      component: News
    },
    {
      path: "/cerereCazare",
      name: "cerereCazare",
      component: AccommodationRequest
    },
    {
      path: "/creareAnunt",
      name: "creareAnunt",
      component: NewsCreator
    }
  ]
});

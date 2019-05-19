import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Register from "./views/Register.vue";
import Article from "./views/Article.vue";
import AccommodationRequest from "./views/AccommodationRequest.vue";
import ArticleCreator from "./views/ArticleCreator.vue";
import Logout from "./components/Logout.vue";

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
      path: "/article/:id",
      name: "article",
      component: Article
    },
    {
      path: "/cerereCazare",
      name: "cerereCazare",
      component: AccommodationRequest
    },
    {
      path: "/articleCreator",
      name: "articleCreator",
      component: ArticleCreator
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout
    }
  ]
});

import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Register from "./views/Register.vue";
import Article from "./views/Article.vue";
import AccommodationRequest from "./views/AccommodationRequest.vue";
import ArticleCreator from "./views/ArticleCreator.vue";
import Logout from "./components/Logout.vue";
import MaleSeatsRegister from "./views/MaleSeatsRegister.vue";
import FemaleSeatsRegister from "./views/FemaleSeatsRegister.vue";
import Applications from "./views/Applications.vue";
import FemaleDistribution from "./views/FemaleDistribution.vue";
import MaleDistribution from "./views/MaleDistribution.vue";
import HostelsStatusPreview from "./views/HostelsStatusPreview";
import SeatsDistribution from "./views/SeatsDistribution.vue";
import ConfirmedSeats from "./views/ConfirmedSeats.vue";
import PublicDistribution from "./views/PublicDistribution.vue";
import AccommodationDisposition from "./views/AccommodationDisposition.vue";
import Useful from "./views/Useful.vue";
import Students from "./views/Students.vue";

import goTo from "vuetify/lib/components/Vuetify/goTo";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  scrollBehavior: () => {
    // if (to.hash) {
    //   scrollTo = to.hash;
    // } else if (savedPosition) {
    //   scrollTo = savedPosition.y;
    // }

    return goTo(0);
  },
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
      path: "/articles",
      name: "articles",
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
      path: "/accommodationRequest",
      name: "accommodationRequest",
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
    },
    {
      path: "/maleSeatsRegister",
      name: "maleSeatsRegister",
      component: MaleSeatsRegister
    },
    {
      path: "/femaleSeatsRegister",
      name: "femaleSeatsRegister",
      component: FemaleSeatsRegister
    },
    {
      path: "/applications",
      name: "applications",
      component: Applications
    },
    {
      path: "/femaleDistribution",
      name: "femaleDistribution",
      component: FemaleDistribution
    },
    {
      path: "/maleDistribution",
      name: "maleDistribution",
      component: MaleDistribution
    },
    {
      path: "/hostelsStatusPreview",
      name: "hostelsStatusPreview",
      component: HostelsStatusPreview
    },
    {
      path: "/allocationsPreview",
      name: "allocationsPreview",
      component: SeatsDistribution
    },
    {
      path: "/confirmedSeats",
      name: "confirmedSeats",
      component: ConfirmedSeats
    },
    {
      path: "/publicDistribution",
      name: "publicDistribution",
      component: PublicDistribution
    },
    {
      path: "/accommodationDisposition",
      name: "accommodationDisposition",
      component: AccommodationDisposition
    },
    {
      path: "/useful",
      name: "useful",
      component: Useful
    },
    {
      path: "/students",
      name: "students",
      component: Students
    }
  ]
});

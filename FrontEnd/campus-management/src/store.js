import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

import router from "./router";
import VueAxios from "vue-axios";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);
Vue.use(VueAxios, axios);

axios.defaults.baseURL = "http://localhost:6600/api";

export default new Vuex.Store({
  state: {
    token: {},
    person: {},
    personRoles: [],
    adminDetails: {},
    studentDetails: {},
    articles: [],
    currentArticle: {},
    students: []
  },
  mutations: {
    SET_TOKEN: (state, token) => {
      state.token.accessToken = token.accessToken;
      state.token.refreshToken = token.refreshToken;
    },
    SET_PERSON: (state, person) => {
      state.person = person;
    },
    SET_PERSON_ROLES: (state, personRoles) => {
      state.personRoles = personRoles;
    },
    SET_ADMIN_DETAILS: (state, adminDetails) => {
      state.adminDetails = adminDetails;
    },
    SET_STUDENT_DETAILS: (state, studentDetails) => {
      state.studentDetails = studentDetails;
    },
    SET_ARTICLES: (state, articles) => {
      articles.forEach(
        article => (
          (article.image = axios.defaults.baseURL + "/Files/" + article.image),
          (article.postedDateTime = new Date(
            article.postedDateTime
          ).toLocaleDateString("ro-RO", {
            year: "numeric",
            month: "long",
            day: "numeric",
            hour: "numeric",
            minute: "numeric"
          }))
        )
      );

      state.articles = articles;
    },
    SET_CURRENT_ARTICLE: (state, article) => {
      article.image = axios.defaults.baseURL + "/Files/" + article.image;
      article.postedDateTime = new Date(
        article.postedDateTime
      ).toLocaleDateString("ro-RO", {
        year: "numeric",
        month: "long",
        day: "numeric",
        hour: "numeric",
        minute: "numeric"
      });
      state.currentArticle = article;
    }
  },
  /////////////////////////////////////////// ACTIONS ////////////////////////////////////////////
  /* eslint-disable no-console */
  actions: {
    getToken({ commit }, credentials) {
      return new Promise((resolve, reject) => {
        axios
          .post("/Authentication/Login", {
            email: credentials.email,
            password: credentials.password
          })
          .then(response => {
            const token = response.data;

            commit("SET_TOKEN", token);
            commit("SET_PERSON", token.person);

            //dispatch("retrieveStudentDetails");

            resolve(response);
          })
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    refreshToken({ commit, state }) {
      axios
        .post("/Authentication/Refresh", {
          email: state.person.email,
          token: state.token.refreshToken
        })
        .then(response => {
          const token = response.data;

          commit("SET_TOKEN", token);

          commit("SET_PERSON", token.person);

          //window.location.reload();
        })
        .catch(error => {
          router.push({ name: "logout" });
          console.log(error);
        });
    },
    destroyToken({ commit, state }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.accessToken;

      axios
        .post("/Authentication/Revoke", {
          token: state.token.accessToken
        })
        // eslint-disable-next-line
        .then(response => {
          commit("SET_TOKEN", null);
          commit("SET_PERSON", null);
          commit("SET_PERSON_ROLES", null);
          commit("SET_STUDENT_DETAILS", null);

          //commit("SET_ADMIN_DETAILS", null);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getPersonRoles({ commit, state }) {
      axios
        .get("/Authentication/" + state.person.id)
        .then(response => {
          commit("SET_PERSON_ROLES", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getStudentDetails({ commit, state }) {
      axios
        .get("/Students/GetByPerson/" + state.person.id)
        .then(response => {
          commit("SET_STUDENT_DETAILS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getAdminDetails({ commit, state }) {
      axios
        .get("/Admins/" + state.person.id)
        .then(response => {
          commit("SET_ADMIN_DETAILS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getArticles(context) {
      axios
        .get("/articles")
        .then(response => {
          //localStorage.clear();
          //window.localStorage.clear();
          context.commit("SET_ARTICLES", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getArticle({ commit }, id) {
      axios
        .get("/articles/" + id)
        .then(response => {
          commit("SET_CURRENT_ARTICLE", response.data);
        })
        .catch(error => {
          //if (error.response.status == 401) {
          //  dispatch("refreshToken");
          //}
          console.log(error + " asta e eroarea mea");
          // console.log(error.response.status);
        });
    },
    addArticle({ state }, formData) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      axios
        .post("/articles", formData, {
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "multipart/form-data"
          }
        })
        .catch(error => {
          console.log(error);
        });
    }
  },
  getters: {
    person: state => state.person,
    isStudent: state => state.personRoles.indexOf("Student") > -1,
    student: state => state.studentDetails,
    isAdmin: state => state.personRoles.indexOf("Admin") > -1,
    articles: state => state.articles,
    article: state => state.currentArticle
  },
  plugins: [createPersistedState()]
});

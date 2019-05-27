import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

import router from "./router";
import VueAxios from "vue-axios";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);
Vue.use(VueAxios, axios);

axios.defaults.baseURL = "http://localhost:6600/api/";

function compareHostels(a, b) {
  const nameA = a.name.toUpperCase();
  const nameB = b.name.toUpperCase();

  let comparison = 0;
  if (nameA > nameB) {
    comparison = 1;
  } else if (nameA < nameB) {
    comparison = -1;
  }
  return comparison;
}

export default new Vuex.Store({
  state: {
    token: {},
    person: {},
    personRoles: [],
    adminDetails: {},
    studentDetails: {},
    articles: [],
    currentArticle: {},
    hostels: [],
    applications: []
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
    SET_STUDENTS: (state, students) => {
      state.students = students;
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
    },
    SET_HOSTELS: (state, hostels) => {
      state.hostels = hostels.sort(compareHostels);
    },
    SET_APPLICATIONS: (state, applications) => {
      state.applications = applications;
    }
  },
  /////////////////////////////////////////// ACTIONS ////////////////////////////////////////////
  /* eslint-disable no-console */
  actions: {
    getToken({ commit, dispatch }, credentials) {
      return new Promise((resolve, reject) => {
        axios
          .post("Authentication/Login", {
            email: credentials.email,
            password: credentials.password
          })
          .then(response => {
            const token = response.data;

            commit("SET_TOKEN", token);
            commit("SET_PERSON", token.person);
            dispatch("getPersonRoles");
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
        .post("Authentication/Refresh", {
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
        "Bearer " + state.token.accessToken;

      axios
        .post("Authentication/Revoke", {
          token: state.token.accessToken
        })
        // eslint-disable-next-line
        .then(response => {
          commit("SET_TOKEN", {});
          commit("SET_PERSON", {});
          commit("SET_PERSON_ROLES", []);
          commit("SET_STUDENT_DETAILS", {});
          commit("SET_ADMIN_DETAILS", {});
          console.log("sunt la distrugerea tokenului");
          localStorage.clear();
          window.localStorage.clear();

          //commit("SET_ADMIN_DETAILS", null);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getPersonRoles({ commit, state, getters, dispatch }) {
      axios
        .get("Authentication/" + state.person.id)
        .then(response => {
          commit("SET_PERSON_ROLES", response.data);
          if (getters.isStudent) dispatch("getStudentDetails");

          if (getters.isAdmin) dispatch("getAdminDetails");
        })
        .catch(error => {
          console.log(error);
        });
    },
    getStudentDetails({ commit, state }) {
      axios
        .get("Students/GetByPerson/" + state.person.id)
        .then(response => {
          commit("SET_STUDENT_DETAILS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getAdminDetails({ commit, state }) {
      axios
        .get("Admins/GetByPerson/" + state.person.id)
        .then(response => {
          commit("SET_ADMIN_DETAILS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getArticles({ commit }) {
      axios
        .get("articles")
        .then(response => {
          commit("SET_ARTICLES", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getArticle({ commit }, id) {
      axios
        .get("articles/" + id)
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
        .post("articles", formData, {
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "multipart/form-data"
          }
        })
        .catch(error => {
          console.log(error);
        });
    },
    getHostels({ commit }) {
      axios
        .get("hostels")
        .then(response => {
          commit("SET_HOSTELS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    addApplication({ state }, application) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      axios.post("Applications", application).catch(error => {
        console.log(error);
      });
    },
    addOrUpdateHostelStatus({ state }, status) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      axios.post("HostelsStatus", status).catch(error => {
        console.log(error);
      });
    },
    addOrUpdateHostelsStatus({ state }, status) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      axios.post("HostelsStatus/bulk", status).catch(error => {
        console.log(error);
      });
    },
    getApplications({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      axios
        .get("Applications")
        .then(response => {
          commit("SET_APPLICATIONS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getStudents({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      axios
        .get("Students")
        .then(response => {
          commit("SET_STUDENTS", response.data);
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
    article: state => state.currentArticle,
    students: state => state.students,
    hostels: state => state.hostels,
    applications: state => state.applications,
    indexById: state => entity => {
      return state[entity].reduce((map, item, index) => {
        // Store the `index` instead of the `item`
        map[item.id] = index;
        return map;
      }, {});
    },
    byId: (state, getters) => (entity, id) => {
      return state[entity][getters.indexById(entity)[id]];
    }
  },
  plugins: [createPersistedState()]
});

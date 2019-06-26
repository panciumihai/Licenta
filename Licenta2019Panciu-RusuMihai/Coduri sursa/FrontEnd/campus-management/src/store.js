import Vue from "vue";
import Vuex from "vuex";
import axios from "axios";

import router from "./router";
import VueAxios from "vue-axios";
import createPersistedState from "vuex-persistedstate";

Vue.use(Vuex);
Vue.use(VueAxios, axios);

axios.defaults.baseURL = "http://localhost:6600/api/";
axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
/* eslint-disable no-console */
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

function compareStudentsGroupsYear(a, b) {
  let comparison = 0;
  if (a.year > b.year) {
    comparison = 1;
  } else if (a.year < b.year) {
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
    applications: [],
    distributions: [],
    hostelsStatus: [],
    allocationsPreview: [],
    unconfirmedStudents: [],
    confirmedStudents: [],
    loading: false,
    stage: {},
    snackbar: {
      active: false,
      timeout: 6000,
      text: "Operatiune efectuata cu succes!",
      color: "success"
    }
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
      console.log(state.currentArticle);
    },
    DELETE_CURRENT_ARTICLE: state => {
      state.currentArticle = {};
    },
    SET_HOSTELS: (state, hostels) => {
      state.hostels = hostels.sort(compareHostels);
    },
    SET_APPLICATIONS: (state, applications) => {
      state.applications = applications;
    },
    SET_DISTRIBUTIONS: (state, distributions) => {
      state.distributions = distributions;
    },
    SET_HOSTELS_STATUS(state, hostelsStatus) {
      state.hostelsStatus = hostelsStatus;
    },
    SET_ALLOCATIONS_PREVIEW(state, allocationsPreview) {
      state.allocationsPreview = allocationsPreview;
    },
    SET_UNCONFIRMED_STUDENTS(state, unconfirmedStudents) {
      state.unconfirmedStudents = unconfirmedStudents;
    },
    SET_CONFIRMED_STUDENTS(state, confirmedStudents) {
      state.confirmedStudents = confirmedStudents;
    },
    SET_STAGE(state, stage) {
      state.stage = stage;
    },
    SET_SNACKBAR(state, snackbar) {
      state.snackbar.text = snackbar.text;
      state.snackbar.color = snackbar.color;
      state.snackbar.active = true;
    },
    HIDE_SNACKBAR(state) {
      state.snackbar.active = false;
    }
  },
  /////////////////////////////////////////// ACTIONS ////////////////////////////////////////////
  actions: {
    getToken({ commit, dispatch, getters }, credentials) {
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
            //dispatch("getPersonRoles");
            //dispatch("retrieveStudentDetails");
            commit("SET_PERSON_ROLES", token.personRoles);
            if (getters.isStudent) dispatch("getStudentDetails");

            if (getters.isAdmin) dispatch("getAdminDetails");

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
          console.log("sunt la distrugerea tokenului");
          //commit("SET_ADMIN_DETAILS", null);
        })
        .catch(error => {
          console.log(error);
        });
      commit("SET_TOKEN", {});
      commit("SET_PERSON", {});
      commit("SET_PERSON_ROLES", []);
      commit("SET_STUDENT_DETAILS", {});
      commit("SET_ADMIN_DETAILS", {});
      localStorage.clear();
      window.localStorage.clear();
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
    getStudentWithHostel({ commit, state }) {
      axios
        .get("Students/GetWithHostel/" + state.studentDetails.id)
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
      commit("DELETE_CURRENT_ARTICLE");
      axios
        .get("articles/" + id)
        .then(response => {
          commit("SET_CURRENT_ARTICLE", response.data);
        })
        .catch(error => {
          console.log(error + " asta e eroarea mea");
        });
    },
    addArticle({ state }, formData) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      return new Promise((resolve, reject) => {
        axios
          .post("articles", formData, {
            headers: {
              "Access-Control-Allow-Origin": "*",
              "Content-Type": "multipart/form-data"
            }
          })
          .then(resolve())
          .catch(error => {
            console.log(error);
            reject();
          });
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
      return new Promise((resolve, reject) => {
        axios
          .post("Applications", application)
          .then(resolve())
          .catch(error => {
            console.log(error);
            reject();
          });
      });
    },
    addOrUpdateHostelStatus({ state }, status) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
      return new Promise((resolve, reject) => {
        axios
          .post("HostelsStatus", status)
          .then(resolve())
          .catch(error => {
            console.log(error);
            reject();
          });
      });
    },
    addOrUpdateHostelsStatus({ state }, status) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

      return new Promise((resolve, reject) => {
        axios
          .post("HostelsStatus/bulk", status)
          .then(response => {
            resolve(response.data);
          })
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    getApplications({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      commit("SET_APPLICATIONS", []);
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

      axios
        .get("Students")
        .then(response => {
          commit("SET_STUDENTS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    addStudent({ state }, student) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      return new Promise((resolve, reject) => {
        axios
          .post("Students", student)
          .then(response => resolve(response))
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    addStudentsAllocations({ state }, studentsAllocations) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      return new Promise((resolve, reject) => {
        axios
          .post("Students/SetStudentsGroup", studentsAllocations)
          .then(response => resolve(response))
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    getDistributions({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      commit("SET_DISTRIBUTIONS", []);
      axios
        .get("Applications/SeatsDistribution")
        .then(response => {
          commit("SET_DISTRIBUTIONS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getHostelsStatusPreview({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      axios
        .get("HostelsStatus/Seats")
        .then(response => {
          commit("SET_HOSTELS_STATUS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getHostelsStatusAllocation({ commit }) {
      //state.loading = true;
      commit("SET_STAGE", {});
      axios
        .get("Stages/Current")
        .then(response => {
          commit("SET_STAGE", response.data);
          //state.loading = false;
          console.log(response.status);
        })
        .catch(error => {
          console.log(error);
        });

      commit("SET_ALLOCATIONS", []);

      axios
        .get("HostelsStatus/SeatsAllocation")
        .then(response => {
          commit("SET_ALLOCATIONS", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    getHostelsStatusAllocationPreview({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      commit("SET_ALLOCATIONS_PREVIEW", []);

      axios
        .get("HostelsStatus/SeatsAllocationPreview")
        .then(response => {
          commit("SET_ALLOCATIONS_PREVIEW", response.data);
        })
        .catch(error => {
          console.log(error);
        });
    },
    addStudentConfirmation({ state }, confirmation) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      return new Promise((resolve, reject) => {
        axios
          .post("Students/Confirmation", confirmation)
          .then(response => {
            resolve(response.data);
          })
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    getConfirmedStudents({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      state.loading = true;
      axios
        .get("Students/ConfirmedStudents")
        .then(response => {
          commit("SET_CONFIRMED_STUDENTS", response.data);
          state.loading = false;
        })
        .catch(error => {
          console.log(error);
        });
    },
    getUnconfirmedStudents({ state, commit }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      state.loading = true;
      axios
        .get("Students/UnconfirmedStudents")
        .then(response => {
          commit("SET_UNCONFIRMED_STUDENTS", response.data);
          state.loading = false;
        })
        .catch(error => {
          console.log(error);
        });
    },
    getStage({ state, commit, dispatch }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;
      //state.loading = true;
      commit("SET_STAGE", {});
      axios
        .get("Stages/Current")
        .then(response => {
          commit("SET_STAGE", response.data);
          //state.loading = false;
          console.log(response.status);
          if (response.status != 204)
            dispatch("getHostelsStatusAllocationPreview");
        })
        .catch(error => {
          console.log(error);
        });
    },
    addStage({ state }, stage) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      return new Promise((resolve, reject) => {
        axios
          .post("Stages", stage)
          .then(response => {
            resolve(response.data);
          })
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },
    publishSeats({ state }) {
      axios.defaults.headers.common["Authorization"] =
        "Bearer " + state.token.accessToken;

      return new Promise((resolve, reject) => {
        axios
          .post("HostelsStatus/Publish")
          .then(response => {
            resolve(response.data);
          })
          .catch(error => {
            console.log(error);
            reject(error);
          });
      });
    },

    showSnackbar({ commit }, snackbar) {
      commit("SET_SNACKBAR", snackbar);
    },
    hideSnackbar({ commit }) {
      commit("HIDE_SNACKBAR");
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
    distributions: state => state.distributions,
    hostelsStatus: state => state.hostelsStatus,
    allocationsPreview: state => state.allocationsPreview,
    confirmedStudents: state => state.confirmedStudents,
    unconfirmedStudents: state => state.unconfirmedStudents,
    stage: state => state.stage,
    maleDistributions: state =>
      state.distributions
        .filter(d => d.gender == "M")
        .sort(compareStudentsGroupsYear),
    femaleDistributions: state =>
      state.distributions
        .filter(d => d.gender == "F")
        .sort(compareStudentsGroupsYear),
    indexById: state => entity => {
      return state[entity].reduce((map, item, index) => {
        // Store the `index` instead of the `item`
        map[item.id] = index;
        return map;
      }, {});
    },
    byId: (state, getters) => (entity, id) => {
      return state[entity][getters.indexById(entity)[id]];
    },
    globalSnackbar: state => state.snackbar
  },
  plugins: [createPersistedState()]
});

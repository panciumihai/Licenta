<template>
  <v-container>
    <v-card>
      <v-layout class="mx-4 pt-3" row wrap>
        <v-flex xs12 sm6 md4>
          <v-select v-model="selectedYear" :items="years" label="An universitar"></v-select>
        </v-flex>
        <v-flex xs12 sm6 md4>
          <v-select v-model="selectedGender" :items="genders" label="Sex"></v-select>
        </v-flex>
        <v-flex xs12 sm12 md4>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Cauta student..."
            single-line
            hide-details
          ></v-text-field>
        </v-flex>
      </v-layout>
      <v-layout child-flex row wrap>
        <v-data-table
          :headers="headers"
          :loading="!isLoaded"
          :items="filteredApplications"
          :search="search"
          v-bind:pagination.sync="pagination"
          class="elevation-1"
        >
          <template v-slot:items="app">
            <td>{{ app.item.studentName }}</td>
            <td class="text-xs-left">{{ app.item.gender }}</td>
            <td class="text-xs-left">{{ app.item.year }}</td>
            <td class="text-xs-left">{{ app.item.score }}</td>
            <td class="text-xs-left">{{ app.item.secondScore }}</td>
            <td class="text-xs-left">{{ app.item.preferences }}</td>
          </template>
        </v-data-table>
      </v-layout>
    </v-card>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      loading: true,
      years: ["1", "2", "3", "4", "5"],
      genders: ["M", "F"],
      selectedYear: null,
      selectedGender: null,
      search: null,
      headers: [
        {
          text: "Nume",
          align: "left",
          sortable: true,
          value: "studentName"
        },
        { text: "Sex", align: "left", value: "gender" },
        { text: "An", align: "left", value: "year" },
        { text: "Punctaj", align: "left", value: "score" },
        { text: "Punctaj 2", align: "left", value: "secondScore" },
        { text: "Preferinte", align: "left", value: "preferences" }
      ],
      pagination: { sortBy: "score", descending: true, rowsPerPage: 10 }
    };
  },
  methods: {
    application(id) {
      return this.$store.getters.byId("applications", id);
      // return this.$store.getters.indexById("hostels")[
      //   "ec89d470-ee40-4756-857f-335aa03a717c"
      // ];
    },
    student(id) {
      return this.$store.getters.byId("students", id);
    }
  },
  computed: {
    isLoaded() {
      if (this.applications.length > 0) return true;
      return false;
    },
    filteredApplications() {
      let apps = this.applications;
      if (this.selectedYear != null)
        apps = apps.filter(a => a.year == this.selectedYear);
      if (this.selectedGender != null)
        apps = apps.filter(a => a.gender == this.selectedGender);
      return apps;
    },
    applications() {
      return this.$store.getters.applications.map(a => {
        let student = this.$store.getters.byId("students", a.studentId);
        let preferences = "";
        a.hostelPreferences.forEach(h => {
          preferences +=
            this.$store.getters
              .byId("hostels", h.hostelId)
              .name.substring(0, 3)
              .toUpperCase() + ", ";
        });
        preferences = preferences.substring(0, preferences.length - 2);
        return {
          id: a.id,
          studentName: student.lastName + " " + student.firstName,
          gender: student.gender,
          year: student.year,
          score: student.score,
          secondScore: student.secondScore,
          preferences: preferences
        };
      });
    }
  },
  mounted() {
    this.$store.dispatch("getStudents");
    this.$store.dispatch("getApplications");
  }
};
</script>

<style>
</style>

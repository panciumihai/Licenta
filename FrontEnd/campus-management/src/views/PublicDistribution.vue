<template>
  <v-container>
    <v-card v-if="filteredUnconfirmedStudents.length > 0">
      <v-layout class="mx-4 pt-3" row wrap>
        <v-flex xs12 sm12 md12>
          <v-layout justify-center>
            <div
              class="title"
            >{{"Turul actual este pentru cererile facute intre "+stage.startDate+" si "+stage.endDate}}</div>
          </v-layout>
        </v-flex>
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
          :items="filteredUnconfirmedStudents"
          :search="search"
          v-bind:pagination.sync="pagination"
          class="elevation-1"
        >
          <template v-slot:items="student">
            <td>{{ student.index+1 }}</td>
            <td>{{ student.item.fullName }}</td>
            <td class="text-xs-left">{{ student.item.gender }}</td>
            <td class="text-xs-left">{{ student.item.year }}</td>
            <td class="text-xs-left">{{ student.item.score }}</td>
            <td class="text-xs-left">{{ student.item.secondScore }}</td>
            <td class="text-xs-left">{{ student.item.hostelName }}</td>
          </template>
        </v-data-table>
      </v-layout>
    </v-card>
    <v-card v-else>
      <v-layout class="py-4" justify-center>
        <div class="display-1">{{"Nu exista nimic de afisat!"}}</div>
      </v-layout>
    </v-card>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      years: ["1", "2", "3", "4", "5"],
      genders: ["M", "F"],
      selectedYear: null,
      selectedGender: null,
      search: null,
      headers: [
        { text: "Nr.", align: "left", value: "gender" },
        {
          text: "Nume si Prenume",
          align: "left",
          sortable: true,
          value: "fullName"
        },
        { text: "Sex", align: "left", value: "gender" },
        { text: "An", align: "left", value: "year" },
        { text: "Punctaj", align: "left", value: "score" },
        { text: "Punctaj 2", align: "left", value: "secondScore" },
        { text: "Camin", align: "left", value: "hostelName" }
      ],
      pagination: { sortBy: "score", descending: true, rowsPerPage: 10 }
    };
  },
  methods: {
    compareStudentsByYear(a, b) {
      let comparison = 0;

      if (a.year < b.year) comparison = -1;
      else if (a.year > b.year) comparison = 1;
      return comparison;
    },
    compareStudentsByGender(a, b) {
      let comparison = 0;
      if (a.gender > b.gender) {
        comparison = -1;
      } else if (a.gender < b.gender) {
        comparison = 1;
      }
      return comparison;
    },
    compareStudentsByScore(a, b) {
      let comparison = 0;
      if (a.score > b.score) {
        comparison = -1;
      } else if (a.score < b.score) {
        comparison = 1;
      }
      return comparison;
    },
    compareStudentsBySecondScore(a, b) {
      let comparison = 0;

      if (a.secondScore > b.secondScore) comparison = -1;
      else if (a.secondScore < b.secondScore) comparison = 1;

      return comparison;
    }
  },
  computed: {
    stage() {
      return this.$store.getters.stage;
    },
    unconfirmedStudents() {
      return this.$store.getters.unconfirmedStudents.map(s => {
        s.fullName = s.lastName + " " + s.firstName;
        return s;
      });
    },
    isLoaded() {
      if (
        this.filteredUnconfirmedStudents.length > 0 ||
        this.$store.getters.loading
      )
        return true;
      return false;
    },
    filteredUnconfirmedStudents() {
      let students = this.unconfirmedStudents;
      if (this.selectedYear != null)
        students = students.filter(a => a.year == this.selectedYear);
      if (this.selectedGender != null)
        students = students.filter(a => a.gender == this.selectedGender);
      return students
        .sort(this.compareStudentsBySecondScore)
        .sort(this.compareStudentsByScore)
        .sort(this.compareStudentsByGender)
        .sort(this.compareStudentsByYear);
    }
  },
  mounted() {
    this.$store.dispatch("getUnconfirmedStudents");
  }
};
</script>

<style>
</style>

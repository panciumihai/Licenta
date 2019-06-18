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
          :items="filteredConfirmedStudents"
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
            <td class="text-xs-left">
              <v-btn dark small flat color="red" @click="confirmation(student.item)">Decazează</v-btn>
            </td>
          </template>
        </v-data-table>
      </v-layout>
    </v-card>
    <StudentRemoveDialog v-model="removeStudentDialog" :selectedStudent="selectedStudent"></StudentRemoveDialog>
  </v-container>
</template>

<script>
import StudentRemoveDialog from "@/components/StudentRemoveDialog";

export default {
  components: {
    StudentRemoveDialog
  },
  data() {
    return {
      removeStudentDialog: false,
      years: ["1", "2", "3", "4", "5"],
      genders: ["M", "F"],
      selectedYear: null,
      selectedGender: null,
      selectedStudent: {},
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
        { text: "Camin", align: "left", value: "hostelName" },
        { text: "", align: "left", value: "confirmed" }
      ],
      pagination: { sortBy: "score", descending: true, rowsPerPage: 10 }
    };
  },
  methods: {
    confirmation(student) {
      this.selectedStudent = student;
      this.removeStudentDialog = true;
    },
    compareStudentsByScores(a, b) {
      let comparison = 0;
      if (a.score > b.score) {
        comparison = -1;
      } else if (a.score < b.score) {
        comparison = 1;
      }

      if (a.score == b.score) {
        if (a.secondScore > b.secondScore) comparison = -1;
        else if (a.secondScore < b.secondScore) comparison = 1;
      }

      return comparison;
    }
  },
  computed: {
    confirmedStudents() {
      return this.$store.getters.confirmedStudents.map(s => {
        s.fullName = s.lastName + " " + s.firstName;
        return s;
      });
    },
    isLoaded() {
      if (
        this.filteredConfirmedStudents.length > 0 ||
        this.$store.getters.loading
      )
        return true;
      return false;
    },
    filteredConfirmedStudents() {
      let students = this.confirmedStudents;
      if (this.selectedYear != null)
        students = students.filter(a => a.year == this.selectedYear);
      if (this.selectedGender != null)
        students = students.filter(a => a.gender == this.selectedGender);
      return students;
    }
  },
  mounted() {
    this.$store.dispatch("getConfirmedStudents");
  }
};
</script>

<style>
</style>

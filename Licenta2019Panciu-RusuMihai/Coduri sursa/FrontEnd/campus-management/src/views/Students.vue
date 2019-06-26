<template>
  <v-container>
    <v-card>
      <v-layout class="mx-4 pt-3" row wrap>
        <v-flex xs12 sm12 md12 class="my-3">
          <v-layout justify-center>
            <div class="title">{{"Numarul total de studenti este "+students.length}}</div>
          </v-layout>
        </v-flex>
        <v-flex xs12 md3 class="px-2">
          <v-btn
            block
            dark
            color="blue"
            @click="addOrUpdateStudentDialog=true"
            depressed
          >Adauga un student</v-btn>
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn
            block
            dark
            color="blue"
            depressed
            @click="$refs.fileInput.click();"
          >Adauga CSV studenti</v-btn>
          <input ref="fileInput" type="file" name="name" style="display: none;" @change="pickFile">
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn block dark color="blue" depressed>
            <JsonCSV :data="json_data">DESCARCA MODEL CSV</JsonCSV>
          </v-btn>
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn block dark color="blue" depressed>Sterge studentii</v-btn>
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
      <v-layout v-if="true" child-flex row wrap>
        <v-data-table
          :headers="headers"
          :loading="!isLoaded"
          :items="filteredStudents"
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
            <td class="text-xs-left">
              <v-btn dark small flat color="blue">Editeaza</v-btn>
              <!-- <v-switch v-model="student.item.confirmed"></v-switch> -->
            </td>
          </template>
        </v-data-table>
      </v-layout>
      <div v-else class="title">{{"Nu exista niciun student adaugat!"}}</div>
    </v-card>
    <AddOrUpdateStudentDialog v-model="addOrUpdateStudentDialog"></AddOrUpdateStudentDialog>
  </v-container>
</template>

<script>
//import XLSX from "xlsx";
import AddOrUpdateStudentDialog from "@/components/AddOrUpdateStudentDialog";
// eslint-disable-next-line
//import * as fs from "fs";
import csvToJson from "convert-csv-to-json";
//import csv from "csvtojson";
import JsonCSV from "vue-json-csv";

export default {
  components: {
    AddOrUpdateStudentDialog,
    JsonCSV
  },
  data() {
    return {
      json_data: [
        {
          year: "",
          nationality: "",
          cnp: "",
          lastName: "",
          firstName: "",
          gender: "",
          email: "",
          score: "",
          secondScore: ""
        }
      ],
      isLoaded: true,
      addOrUpdateStudentDialog: false,
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
        { text: "Confirmare", align: "left", value: "confirmed" }
      ],
      pagination: { sortBy: "year", descending: false, rowsPerPage: 10 },

      addedStudents: [],
      file: {}
    };
  },
  methods: {
    pickFile(e) {
      const files = e.target.files;
      if (files[0] !== undefined) {
        this.file = files[0];
        console.log(files[0]);
        let json = csvToJson.getJsonFromCsv(files[0].name);
        console.log(json);
      }
    },
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
    filteredStudents() {
      let filtered = this.students;
      if (this.selectedYear != null)
        filtered = filtered.filter(a => a.year == this.selectedYear);
      if (this.selectedGender != null)
        filtered = filtered.filter(a => a.gender == this.selectedGender);
      return filtered;
    },
    students() {
      let students = this.$store.getters.students.map(s => {
        s.fullName = s.lastName + " " + s.firstName;
        return s;
      });

      return students
        .sort(this.compareStudentsBySecondScore)
        .sort(this.compareStudentsByScore)
        .sort(this.compareStudentsByGender)
        .sort(this.compareStudentsByYear);
    }
  },
  mounted() {
    this.$store.dispatch("getStudents");
  }
};
</script>

<style>
</style>

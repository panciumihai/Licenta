<template>
  <v-container>
    <v-card>
      <v-layout class="mx-4 pt-3" row wrap>
        <v-flex xs12 sm12 md12 class="my-3">
          <v-layout justify-center>
            <div
              class="title"
            >{{"Turul actual este pentru cererile facute intre "+stage.startDate+" si "+stage.endDate}}</div>
          </v-layout>
        </v-flex>
        <v-flex xs12 md3 class="px-2">
          <v-btn
            block
            dark
            color="blue"
            depressed
            @click="applicationFilterDialog = true"
          >Genereaza tur</v-btn>
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn
            block
            dark
            color="blue"
            depressed
            @click="publicSeatsDialog = true; setStudentsAllocation()"
          >Posteaza locurile</v-btn>
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn block dark color="blue" depressed @click="downloadExcel()">DESCARCA EXCEL</v-btn>
        </v-flex>
        <v-flex xs md3 class="px-2">
          <v-btn
            block
            dark
            color="blue"
            depressed
            @click="applicationFilterDialog = true"
          >Descarca pdf</v-btn>
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
      <v-layout v-if="isStageExists" child-flex row wrap>
        <v-data-table
          :headers="headers"
          :loading="!isLoaded"
          :items="filteredAllocations"
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
              <v-btn dark small flat color="blue" @click="confirmation(student.item)">Confirma</v-btn>
              <!-- <v-switch v-model="student.item.confirmed"></v-switch> -->
            </td>
          </template>
        </v-data-table>
      </v-layout>
      <div v-else class="title">{{"Nu exista niciun tur."}}</div>
    </v-card>

    <StageGenerateDialog v-model="applicationFilterDialog"></StageGenerateDialog>
    <StudentConfirmationDialog
      v-model="studentConfimationDialog"
      :selectedStudent="selectedStudent"
    ></StudentConfirmationDialog>
    <PublicSeatsDialog
      v-if="studentsAllocationModels.length > 0"
      v-model="publicSeatsDialog"
      :stage="stage"
      :studentsAllocations="studentsAllocationModels"
    ></PublicSeatsDialog>
  </v-container>
</template>

<script>
import StageGenerateDialog from "@/components/StageGenerateDialog";
import StudentConfirmationDialog from "@/components/StudentConfirmationDialog";
import PublicSeatsDialog from "@/components/PublicSeatsDialog";

import XLSX from "xlsx";

export default {
  components: {
    StageGenerateDialog,
    StudentConfirmationDialog,
    PublicSeatsDialog
  },
  data() {
    return {
      studentConfimationDialog: false,
      applicationFilterDialog: false,
      publicSeatsDialog: false,
      loadingDialog: false,
      studentsAllocationModels: [],
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
        { text: "Confirmare", align: "left", value: "confirmed" }
      ],
      pagination: { sortBy: "year", descending: false, rowsPerPage: 10 }
    };
  },
  methods: {
    downloadExcel() {
      var auxYear = this.selectedYear;
      var auxGender = this.selectedGender;
      var auxSearch = this.search;
      var studentsExcel = XLSX.utils.book_new();
      for (var i = 1; i <= 5; ++i) {
        this.genders.forEach(g => {
          this.selectedYear = i;
          this.selectedGender = g;
          let sheetData = this.filteredAllocations.map(s => {
            return {
              "An studiu": s.year,
              Nationalitate: s.nationality,
              "Nume si prenume": s.fullName,
              Sex: s.gender,
              CNP: s.cnp,
              "Punctaj 1": s.score,
              "Punctaj 2": s.secondScore,
              Camin: s.hostelName
            };
          });
          var ws = XLSX.utils.json_to_sheet(sheetData); //, { header: ["hostelName", "fullName"]}
          var wscols = [
            { wch: 10 },
            { wch: 15 },
            { wch: 20 },
            { wch: 5 },
            { wch: 15 },
            { wch: 10 },
            { wch: 10 },
            { wch: 15 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 },
            { wch: 20 }
          ];
          ws["!cols"] = wscols;

          XLSX.utils.book_append_sheet(
            studentsExcel,
            ws,
            "Anul " + i + " " + g
          );
        });
      }
      this.selectedYear = auxYear;
      this.selectedGender = auxGender;
      this.search = auxSearch;
      XLSX.writeFile(studentsExcel, "repartizari.xlsx");
    },
    setStudentsAllocation() {
      this.studentsAllocationModels = this.studentsAllocations.map(s => {
        return {
          id: s.id,
          confirmed: false,
          studentsGroupId: s.studentsGroupId
        };
      });
    },
    generateStage(valid) {
      this.applicationFilterDialog = valid;
    },
    confirmation(student) {
      this.selectedStudent = student;
      this.studentConfimationDialog = true;
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
    allocationsPreview() {
      if (this.isStageExists == false) return [];
      return this.$store.getters.allocationsPreview;
    },
    stage() {
      let stage = this.$store.getters.stage;
      stage.startDate = new Date(stage.startDate).toLocaleDateString("ro-RO", {
        year: "numeric",
        month: "long",
        day: "numeric"
      });
      stage.endDate = new Date(stage.endDate).toLocaleDateString("ro-RO", {
        year: "numeric",
        month: "long",
        day: "numeric"
      });

      return stage;
    },
    isStageExists() {
      return Object.keys(this.stage).length > 0;
    },
    isLoaded() {
      if (this.filteredAllocations.length > 0) return true;
      return false;
    },
    studentsAllocations() {
      let students = this.allocationsPreview.map(s => {
        s.fullName = s.lastName + " " + s.firstName;
        return s;
      });

      return students
        .sort(this.compareStudentsBySecondScore)
        .sort(this.compareStudentsByScore)
        .sort(this.compareStudentsByGender)
        .sort(this.compareStudentsByYear);
    },
    filteredAllocations() {
      let allocations = this.studentsAllocations;
      if (this.selectedYear != null)
        allocations = allocations.filter(a => a.year == this.selectedYear);
      if (this.selectedGender != null)
        allocations = allocations.filter(a => a.gender == this.selectedGender);
      return allocations;
    }
  },
  mounted() {
    this.$store.dispatch("getStage");
    //this.$store.dispatch("getHostelsStatusAllocationPreview");
  }
};
</script>

<style>
</style>

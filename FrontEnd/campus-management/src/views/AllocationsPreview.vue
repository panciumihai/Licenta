<template>
  <v-container>
    <Loading v-if="!isLoaded"></Loading>
    <v-card v-else class="px-4 pt-3" row wrap>
      <v-card-title>
        <v-layout class="mx-4 pt-3" row wrap>
          <v-flex>
            <v-btn dark color="blue">Afiseaza locurile</v-btn>
          </v-flex>
          <v-flex>
            <v-btn dark color="blue">Elimina cererile necofirmate</v-btn>
          </v-flex>
          <v-flex>
            <v-btn dark color="blue">Descarca excel</v-btn>
          </v-flex>
          <v-flex>
            <v-btn dark color="blue">Importa excel</v-btn>
          </v-flex>
        </v-layout>
        <v-layout class="mx-4 pt-3" row wrap>
          <v-flex xs12 sm6 md6>
            <v-select v-model="selectedYear" :items="years" label="An universitar"></v-select>
          </v-flex>
          <v-flex xs12 sm6 md6>
            <v-select v-model="selectedGender" :items="genders" label="Sex"></v-select>
          </v-flex>
        </v-layout>
      </v-card-title>
      <v-layout class="my-2" row align-center wrap>
        <v-flex md1>
          <div class="title">{{ "Nr."}}</div>
        </v-flex>
        <v-flex md3>
          <div class="title">{{ "Nume si Prenume"}}</div>
        </v-flex>
        <v-flex md1>
          <div class="title">{{ "Sex" }}</div>
        </v-flex>
        <v-flex md1>
          <div class="title">{{ "An" }}</div>
        </v-flex>
        <v-flex md1>
          <div class="title">{{ "P1" }}</div>
        </v-flex>
        <v-flex md1>
          <div class="title">{{ "P2" }}</div>
        </v-flex>
        <v-flex md3>
          <div class="title">{{ "Camin" }}</div>
        </v-flex>
        <v-flex md1>
          <div class="title">{{ "Confirmat" }}</div>
        </v-flex>
        <v-flex md12 class="mt-4">
          <v-divider></v-divider>
        </v-flex>
      </v-layout>
      <StudentAllocation
        v-for="(student,index) in filteredAllocations"
        :key="student.id"
        :student="student"
        :index="index"
      ></StudentAllocation>
    </v-card>
  </v-container>
</template>

<script>
import StudentAllocation from "@/components/StudentAllocation";
import Loading from "@/components/Loading";

export default {
  components: {
    StudentAllocation,
    Loading
  },
  data() {
    return {
      selectedYear: "1",
      selectedGender: "M",
      years: ["1", "2", "3", "4", "5"],
      genders: ["M", "F"]
    };
  },
  methods: {
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
    allocationsPreview() {
      return this.$store.getters.allocationsPreview;
    },
    isLoaded() {
      if (this.filteredAllocations.length > 0) return true;
      return false;
    },
    studentsAllocations() {
      let students = [];
      this.allocationsPreview.forEach(hostelStatus => {
        hostelStatus.studentsGroups.forEach(studentsGroup => {
          studentsGroup.students.forEach(student => {
            student.hostelName = this.$store.getters.byId(
              "hostels",
              hostelStatus.hostelId
            ).name;
            students.push(student);
          });
        });
      });
      return students.sort(this.compareStudentsByScores);
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
    this.$store.dispatch("getHostelsStatusAllocationPreview");
  }
};
</script>

<style>
</style>

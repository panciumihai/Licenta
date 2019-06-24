<template>
  <v-card class="pa-3">
    <v-layout row wrap>
      <v-flex xs12 md12>
        <v-layout justify-center>
          <div class="headline mb-3">{{ "Caminul " + hostel.name }}</div>
        </v-layout>
        <v-layout justify-center>
          <div
            class="title mb-3 red--text"
            v-show="femaleTypedSeats != hostelStatus.femaleSeats"
          >{{ "Locurile introduse pentru fete sunt diferite de total! Locuri scrise "+femaleTypedSeats +" iar totalul este " + hostelStatus.femaleSeats }}</div>
        </v-layout>
        <v-layout justify-center>
          <div
            class="title mb-3 red--text"
            v-show="maleTypedSeats != hostelStatus.maleSeats"
          >{{ "Locurile introduse pentru baieti sunt diferite de total! Locuri scrise "+maleTypedSeats +" iar totalul este " + hostelStatus.maleSeats }}</div>
        </v-layout>
      </v-flex>
      <v-flex xs12 md2>
        <v-layout class="pb-1" row align-center justify-center fill-height>
          <div class="title font-weight-light">{{ "Fete (Total "+hostelStatus.femaleSeats+"):" }}</div>
        </v-layout>
      </v-flex>
      <v-flex md2 v-for="studentsGroup in femaleStudentsGroups" :key="studentsGroup.id">
        <v-text-field
          class="mx-3"
          :label="yearName(studentsGroup.year)"
          v-model="studentsGroup.seats"
          mask="####"
        ></v-text-field>
      </v-flex>
      <v-flex class="my-4" xs12 md12>
        <v-divider></v-divider>
      </v-flex>
      <v-flex xs12 md2>
        <v-layout class="pb-2" row align-center justify-center fill-height>
          <div class="title font-weight-light">{{ "Baieti (Total "+hostelStatus.maleSeats+"):" }}</div>
        </v-layout>
      </v-flex>
      <v-flex md2 v-for="studentsGroup in maleStudentsGroups" :key="studentsGroup.id">
        <v-text-field
          class="mx-3"
          :label="yearName(studentsGroup.year)"
          v-model="studentsGroup.seats"
          mask="####"
        ></v-text-field>
      </v-flex>
    </v-layout>
  </v-card>
</template>

<script>
export default {
  props: {
    hostelStatus: {}
  },
  methods: {
    yearName(year) {
      if (year == "1") {
        return "Anul 1";
      }
      if (year == "2") {
        return "Anul 2";
      }
      if (year == "3") {
        return "Anul 3";
      }
      if (year == "4") {
        return "Master 1";
      }
      return "Master 2";
    },
    genderName(gender) {
      if (gender == "F") return "Fete";
      return "Baieti";
    },
    compareStudentsGroupsYear(a, b) {
      let comparison = 0;
      if (a.year > b.year) {
        comparison = 1;
      } else if (a.year < b.year) {
        comparison = -1;
      }
      return comparison;
    }
  },
  computed: {
    hostel() {
      return this.$store.getters.byId("hostels", this.hostelStatus.hostelId);
    },
    maleStudentsGroups() {
      return this.hostelStatus.studentsGroups
        .filter(s => s.gender == "M")
        .sort(this.compareStudentsGroupsYear);
    },
    maleTypedSeats() {
      let total = 0;
      this.maleStudentsGroups.forEach(g => {
        let numar = parseInt(g.seats);
        if (!isNaN(numar)) total += numar;
      });
      return total;
    },
    femaleStudentsGroups() {
      return this.hostelStatus.studentsGroups
        .filter(s => s.gender == "F")
        .sort(this.compareStudentsGroupsYear);
    },
    femaleTypedSeats() {
      let total = 0;
      this.femaleStudentsGroups.forEach(g => {
        let numar = parseInt(g.seats);
        if (!isNaN(numar)) total += numar;
      });
      return total;
    }
  }
};
</script>

<style>
</style>

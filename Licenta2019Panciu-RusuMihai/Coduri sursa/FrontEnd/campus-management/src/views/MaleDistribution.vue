<template>
  <v-container>
    <v-card class="mb-3">
      <v-layout class="pa-3" row justify-space-between fill-height>
        <div class="headline font-weight-regular">{{"Numar total studenti: "+totalMaleStudents}}</div>
        <div class="headline font-weight-regular">{{"BAIETI"}}</div>
        <div class="headline font-weight-regular">{{"Numar total cereri: "+totalMaleApplications}}</div>
      </v-layout>
    </v-card>
    <YearDistribution
      class="mb-3"
      v-for="distribution in distributions"
      :key="distribution.year + distribution.gender"
      :distribution="distribution"
    ></YearDistribution>
  </v-container>
</template>

<script>
import YearDistribution from "@/components/YearDistribution";

export default {
  components: {
    YearDistribution
  },
  computed: {
    distributions() {
      return this.$store.getters.maleDistributions;
    },
    totalMaleStudents() {
      return this.distributions.reduce((a, b) => +a + +b.yearStudentsNumber, 0);
    },
    totalMaleApplications() {
      return this.distributions.reduce(
        (a, b) => +a + +b.yearApplicationsNumber,
        0
      );
    }
  },
  mounted() {
    this.$store.dispatch("getDistributions");
  }
};
</script>

<style>
</style>

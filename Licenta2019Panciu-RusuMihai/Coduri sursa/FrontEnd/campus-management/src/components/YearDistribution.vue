<template>
  <v-card class="pa-3">
    <v-layout row wrap>
      <v-flex xs12 sm6>
        <div
          class="headline"
        >{{ "Distributie "+yearName(distribution.year)+" - "+genderName(distribution.gender)}}</div>
      </v-flex>
      <v-flex xs12 sm6>
        <v-layout row justify-end fill-height>
          <v-card flat class="mt-1 ml-3" color="blue" width="12" height="12"></v-card>
          <div class="ml-1 text">Procent studenti</div>
          <v-card flat color="yellow darken-2" class="mt-1 ml-3" width="12" height="12"></v-card>
          <div class="ml-1 text">Procent cereri</div>
          <v-card flat color="red" class="mt-1 ml-3" width="12" height="12"></v-card>
          <div class="ml-1 text">Procent de locuri pentru acest an</div>
        </v-layout>
      </v-flex>
      <v-layout row wrap>
        <v-flex xs12 sm4 class="pt-4">
          <div class="text-sm-center">
            <v-progress-circular
              :rotate="270"
              :size="100"
              :width="15"
              :value="distribution.yearStudentsPercentage*100"
              color="blue"
            >{{ Number((distribution.yearStudentsPercentage*100).toFixed(2)) +"%" }}</v-progress-circular>
          </div>
        </v-flex>
        <v-flex xs12 sm4 class="pt-4">
          <div class="text-xs-center">
            <v-progress-circular
              :rotate="270"
              :size="100"
              :width="15"
              :value="distribution.yearApplicationsPercentage*100"
              color="yellow darken-2"
            >{{ Number((distribution.yearApplicationsPercentage*100).toFixed(2)) +"%" }}</v-progress-circular>
          </div>
        </v-flex>
        <v-flex xs12 sm4 class="pt-4">
          <div class="text-xs-center">
            <v-progress-circular
              :rotate="270"
              :size="100"
              :width="15"
              :value="distribution.yearPercentageMean*100"
              color="red"
            >{{ Number((distribution.yearPercentageMean*100).toFixed(2)) +"%" }}</v-progress-circular>
          </div>
        </v-flex>
        <v-flex>
          <v-layout row justify-space-between fill-height>
            <div
              class="mt-3 title font-weight-regular"
            >{{"Numar studenti an: "+distribution.yearStudentsNumber}}</div>
            <div
              class="mt-3 title font-weight-regular"
            >{{"Numar cereri an: "+distribution.yearApplicationsNumber}}</div>
          </v-layout>
        </v-flex>
      </v-layout>
    </v-layout>
  </v-card>
</template>

<script>
export default {
  props: {
    distribution: {}
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
    }
  }
};
</script>

<style>
</style>

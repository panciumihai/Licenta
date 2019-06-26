<template>
  <v-layout class="my-2" row align-center wrap>
    <v-flex md1>
      <div class="subheading">{{ index+1 }}</div>
    </v-flex>
    <v-flex md3>
      <div class="subheading font-weight-medium">{{ student.firstName + " " + student.lastName }}</div>
    </v-flex>
    <v-flex md1>
      <div class="subheading">{{ student.gender }}</div>
    </v-flex>
    <v-flex md1>
      <div class="subheading">{{ student.year }}</div>
    </v-flex>
    <v-flex md1>
      <div class="subheading">{{ student.score }}</div>
    </v-flex>
    <v-flex md1>
      <div class="subheading">{{ student.secondScore }}</div>
    </v-flex>
    <v-flex md3>
      <div class="subheading">{{ student.hostelName }}</div>
    </v-flex>
    <v-flex md1>
      <v-switch v-model="student.confirmed"></v-switch>
    </v-flex>
    <v-flex md12>
      <v-divider></v-divider>
    </v-flex>
  </v-layout>
</template>

<script>
export default {
  props: {
    student: {},
    index: {}
  },
  computed: {}
};
</script>

<style>
</style>

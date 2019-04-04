<template>
  <div class="flex-exercises">
    <v-container fluid class="pa-0 ma-0">
      <v-layout row wrap fill-height>
        <v-flex xs12 md6>
          <v-layout row wrap align-center justify-center fill-height>
            <v-flex xs6 md3>
              <h1 style="background-color: coral;">Prima parte1</h1>
            </v-flex>
            <v-flex xs6 md9>
              <v-layout row wrap>
                <v-flex xs12 md9>
                  <h1 style="background-color: purple;">Prima parte11</h1>
                </v-flex>
                <v-flex xs12 md3>
                  <h1 style="background-color: red;">Prima parte12</h1>
                </v-flex>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-flex>
        <v-flex xs12 md6>
          <v-layout column wrap>
            <v-flex>
              <h1 style="background-color: blue;">Subparte1</h1>
            </v-flex>
            <v-flex>
              <h1 style="background-color: pink;">Subparte2</h1>
            </v-flex>
          </v-layout>
        </v-flex>
        <v-flex xs12 md6>
          <h1>A treia parte</h1>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
export default {};
</script>

<style>
</style>


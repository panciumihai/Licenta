<template>
  <div class="news">
    <v-container>
      <v-card>
        <v-card-title class="mx-3 mt-3 mb-1 pb-1 display-2">{{ news.title }}</v-card-title>
        <v-card-text class="mt-1">
          <v-layout row wrap>
            <v-flex xs12 md12 class="pb-3 px-3">
              <v-layout align-start justify-start>
                <div class="subheading">{{news.author}}</div>
                <div class="subheading font-weight-thin ml-1">a postat pe {{news.date}}</div>
              </v-layout>
            </v-flex>
            <v-flex xs12 md12 class="pb-3 px-3 mb-3">
              <v-img :src="news.image" :lazy-src="news.image" aspect-ratio="2.75"></v-img>
            </v-flex>
            <v-flex xs12 md12 class="pb-3 px-3">
              <div class="title font-weight-regular">{{ news.content }}</div>
            </v-flex>
          </v-layout>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  data() {
    return {
      news: {
        title: "Anunt important legat de cazarile de pe data de 22 septembrie",
        date: "22-02-2019",
        image: "http://campus.tuiasi.ro/wp-content/uploads/DSC_0173-min-1.jpg",
        author: "Popescu Marian",
        content:
          "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque sed ex tempus, iaculis lacus eu, consectetur ligula. Proin pharetra, ante vitae congue placerat, ipsum lacus maximus tellus, pretium imperdiet tellus urna in arcu. Vivamus elementum fringilla ex at ultricies. Nam volutpat tortor eget nisl viverra venenatis."
      }
    };
  }
};
</script>

<style>
</style>

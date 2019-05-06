<template>
  <div id="pageEditor">
    <v-container>
      <v-card>
        <v-card-title class="title mb-1
        ">Scrieti anuntul dorit</v-card-title>
        <v-card-text class="mt-0">
          <v-layout column wrap>
            <v-flex>
              <v-text-field v-model="title" prepend-icon="title" label="Titlu"></v-text-field>
            </v-flex>
            <v-flex align-center align-content-center justify-center align-content-space-around>
              <img :src="imageUrl" aspect-ratio="2.75" v-if="imageUrl">
            </v-flex>
            <v-flex>
              <v-text-field
                label="Alege o imagine"
                @click="pickFile"
                v-model="imageName"
                prepend-icon="attach_file"
              ></v-text-field>
              <input
                type="file"
                style="display: none"
                ref="image"
                accept="image/*"
                @change="onFilePicked"
              >
            </v-flex>
            <v-flex>
              <quill-editor v-model="content" :options="config" output="html"></quill-editor>
            </v-flex>
            <v-flex>
              <v-layout justify-center>
                <v-btn class="mt-3" dark color="blue">Posteaza</v-btn>
              </v-layout>
            </v-flex>
          </v-layout>
        </v-card-text>
      </v-card>
    </v-container>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      content: "",
      config: {
        placeholder: "Scrieti anuntul dumneavoastra..."
      },
      title: "",
      imageName: "",
      imageUrl: "",
      imageFile: ""
    };
  },
  methods: {
    pickFile() {
      this.$refs.image.click();
    },

    onFilePicked(e) {
      const files = e.target.files;
      if (files[0] !== undefined) {
        this.imageName = files[0].name;
        if (this.imageName.lastIndexOf(".") <= 0) {
          return;
        }
        const fr = new FileReader();
        fr.readAsDataURL(files[0]);
        fr.addEventListener("load", () => {
          this.imageUrl = fr.result;
          this.imageFile = files[0]; // this is an image file that can be sent to server...
        });
      } else {
        this.imageName = "";
        this.imageFile = "";
        this.imageUrl = "";
      }
    }
  },
  created: {}
};
</script>

<style>
</style>

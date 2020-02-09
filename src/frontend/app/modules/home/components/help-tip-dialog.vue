<template>
<v-layout row justify-center style="position: relative;">
    <v-dialog id="dialog-help-tip" v-model="dialog" lazy absolute max-width="600px">
        <v-card>
            <v-card-title class="headline">{{ title }}</v-card-title>
            <v-card-text>
                <ul class="tags">
                    <li v-for="red in content.split('.')">{{ red }}</li>
                </ul>
       
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn class="secondary--text darken-1" flat="flat" @click.native="zatvori(false)">{{ okDugme }}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</v-layout>
</template>

<script>
import Identity from "auth/identity";
export default {
  name: "HelpTipDialog",
  props: {
    title: {
      default: null
    },
    content: {
      default: null
    },
    okDugme: {
      default: "Uredu"
    }
 
    
  },
  data() {
    return {
      dialog: true,
      identity: Identity
    };
  },
  created() {
    window.addEventListener("keyup", evt => {
      if (evt.keyCode == 27) this.zatvori();
    });
    window.addEventListener("click", () => {
      this.zatvori();
    });
  },
 
  methods: {
    zatvori(result) {
      this.$emit("zatvori", result);
    }
  }
};
</script>

<style scoped>
ul {
  padding-left: 0px;
}
ul li {
  list-style: none;
  padding-bottom: 5px;
}

ul li:after {
  content: ".";
}

ul.tags li:last-child:after {
  content: "";
}
</style>

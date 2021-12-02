<template>
  <teleport to="body">
    <q-dialog v-model="showDialog" persistent v-bind="$attrs">
      <base-card class="q-pa-sm hide-scrollbar" style="width: 75vw">
        <q-card-section class="row justify-evenly q-ma-none">
          <p class="col-10 text-h6 text-center q-ma-none text-bold">
            {{ title }}
          </p>
          <q-btn class="col-1" icon=" close" flat round dense v-close-popup />
        </q-card-section>
        <q-separator />

        <q-card-section class="q-pa-md">
          <slot></slot>
        </q-card-section>

        <q-separator />

        <q-card-actions class="column items-end q-pa-sm">
          <div>
            <base-button @click="$emit('submit')">Submit</base-button>
          </div>
        </q-card-actions>
      </base-card>
    </q-dialog>
  </teleport>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BaseCard from 'components/ui/BaseCard.vue';
import BaseButton from 'components/ui/BaseButton.vue';

export default defineComponent({
  inheritAttrs: false,
  emits: ['update:modelValue', 'submit'],
  data() {
    return {};
  },
  components: {
    BaseCard,
    BaseButton
  },
  props: {
    title: {
      type: String,
      required: true
    },
    modelValue: {
      type: Boolean,
      required: true
    },
    width: String,
    disableClose: Boolean
  },
  computed: {
    showDialog: {
      get(): boolean {
        return this.modelValue;
      },
      set(value: boolean) {
        this.$emit('update:modelValue', value);
      }
    }
  }
});
</script>

<style lang="scss" scoped></style>

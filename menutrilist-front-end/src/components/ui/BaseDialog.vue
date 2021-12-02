<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide">
    <base-card style="width: 300px">
      <q-card-section>
        <p class="text-center text-h6 text-bold">{{ title }}</p>
        <p class="text-center">{{ body }}</p>
      </q-card-section>

      <q-card-actions align="right">
        <base-button
          color="primary"
          label="Close"
          @click="onCancelClick"
          flat
        />
        <base-button color="primary" label="OK" @click="onOKClick" />
      </q-card-actions>
    </base-card>
  </q-dialog>
</template>

<script>
import { useDialogPluginComponent } from 'quasar';
import BaseCard from './BaseCard.vue';
import BaseButton from './BaseButton.vue';

export default {
  props: {
    title: {
      type: String,
      required: true
    },
    body: {
      type: String,
      required: true
    }
  },
  emits: [...useDialogPluginComponent.emits],
  setup() {
    const { dialogRef, onDialogHide, onDialogOK, onDialogCancel } =
      useDialogPluginComponent();
    return {
      dialogRef,
      onDialogHide,
      onOKClick() {
        onDialogOK();
      },
      onCancelClick: onDialogCancel
    };
  },
  components: { BaseCard, BaseButton }
};
</script>

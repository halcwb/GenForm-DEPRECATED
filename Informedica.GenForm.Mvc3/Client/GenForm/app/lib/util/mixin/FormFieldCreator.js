Ext.define('GenForm.lib.util.mixin.FormFieldCreator', {
    createTextField: function (config) {
        var me = this;
        return me.createField('Ext.form.Text', config);
    },

    createNumberField: function (config) {
        var me = this;
        return me.createField('Ext.form.Number', config);
    },

    createComboBox: function (config) {
        var me = this;
        //noinspection JSUnusedGlobalSymbols
        config = Ext.applyIf(config, {queryMode: false, editable: false});
        return me.createField('GenForm.lib.view.component.EditableComboBox', config);
    },

    createField: function (field, config) {
        var me = this;
        me.fields[config.name] = Ext.create(field, config);
        return me.fields[config.name];
    }
});
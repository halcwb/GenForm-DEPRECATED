Ext.define('GenForm.lib.util.mixin.FormFieldCreator', {

    createTextField: function (config) {
        var me = this;
        return me.createField('Ext.form.Text', config);
    },

    createNumberField: function (config) {
        var me = this;
        return me.createField('Ext.form.Number', config);
    },

    createHiddenField: function (config) {
        var me = this;
        return me.createField('Ext.form.field.Hidden', config);
    },

    createComboBox: function (config) {
        var me = this, field;
        //noinspection JSUnusedGlobalSymbols
        field = config.keyValue ? 'GenForm.lib.view.component.KeyValueCombo' : '';
        field = config.idName ? 'GenForm.lib.view.component.IdNameCombo' : '';
        if (field == '') field = 'Ext.form.field.ComboBox';

        return me.createField(field, config);
    },

    createField: function (field, config) {
        var me = this;

        if (!me.fields) me.fields = {};
        
        me.fields[config.name] = Ext.create(field, config);
        return me.fields[config.name];
    },

    createTab: function (tab, config) {
        var me = this;

        if (!me.tabs) me.tabs = {};

        me.tabs[config.name] = Ext.create(tab, config);

        return me.tabs[config.name];
    }
});
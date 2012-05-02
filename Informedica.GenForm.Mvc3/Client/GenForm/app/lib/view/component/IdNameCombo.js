Ext.define('GenForm.lib.view.component.IdNameCombo', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.idnamecombo', 'widget.idnamecombobox'],

    displayField: 'Name',
    valueField: 'Name',
    editable: true,
    typeAhead: true,
    queryMode: 'local',

    constructor: function (config) {
        var me = this;

        if(config.directFn) {
            config.store = me.createStore(config.directFn);
        }

        if (!config || !config.store) {
            Ext.Error.raise('Combobox needs a store');
        }

        me.initConfig(config);
        me.callParent(arguments);
        return me;
    },

    initComponent: function () {
        var me = this;

        me.callParent(arguments);
    },

    createStore: function (directFn) {
        var store = Ext.create('GenForm.store.common.IdName', { directFn: directFn });
        store.load();
        return store;
    },

    getIdValue: function () {
        var me = this;

        return me.store.findRecord('Name', me.getValue()).data.Id;
    }

});
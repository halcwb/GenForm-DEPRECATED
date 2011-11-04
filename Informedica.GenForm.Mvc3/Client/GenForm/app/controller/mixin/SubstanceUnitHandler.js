Ext.define('GenForm.controller.mixin.SubstanceUnitHandler', {

    getSubstanceUnit: function (button) {
        return button.up('panel').down('form').getUnit();
    },

    onAddSubstanceUnit: function () {
        var me = this,
            window = me.getSubstanceUnitWindow(me.createEmptySubstanceUnit()).show();

        window.setTitle('Nieuwe stof eenheid');
        window.show();
    },

    onEditSubstanceUnit: function (button) {
        var me = this,
            form = button.findParentByType('productsubstanceform'),
            unit = form.fields.Unit.findRecord('Name', form.fields.Unit.getValue()),
            window = me.getUnitWindow(unit);

        window.show();
    },

    getSubstanceUnitWindow: function (unit) {
        var me = this, window;

        window = me.createSubstanceUnitWindow();
        if (!unit) {
            me.loadEmptySubstanceUnit(window);
        } else {
            window.setTitle('Bewerk eenheid: ' + unit.data.Name);
            window.loadWithUnit(unit);
        }
        return window;
    },

    createSubstanceUnitWindow: function () {
        return Ext.create('GenForm.view.product.SubstanceUnitWindow');
    },

    addSubstanceUnitToStore: function (unit) {
        var me = this,
            store = me.getSubstanceUnitStore();

        store.add({Name: unit});
    },

    saveSubstanceUnit: function (button) {
        var me = this,
            unit = me.getSubstanceUnit(button);

        GenForm.server.UnitTest.SaveUnit(unit.data, {scope: me, callback:me.onSubstanceUnitSaved});
    },

    onSubstanceUnitSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('substanceunitwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Stof eenheid opgeslagen: ', result.data.Name);
            me.addSubstanceUnitToStore(result.data.Name);
            window.close();
        } else {
            Ext.MessageBox.alert('Unit could not be saved: ', result.message);
        }
    },

    getSubstanceUnitStore: function () {
        return Ext.ComponentQuery.query('productsubstanceform')[0].fields.Unit.store;
    },

    createEmptySubstanceUnit: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Unit');
    },

    loadEmptySubstanceUnit: function (window) {
        var me = this;
        window.loadWithUnit(me.createEmptyUnit());
    }

});
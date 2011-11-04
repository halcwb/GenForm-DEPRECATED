Ext.define('GenForm.controller.mixin.UnitHandler', {

    getUnit: function (button) {
        return button.up('panel').down('form').getUnit();
    },

    onAddUnit: function () {
        var me = this,
            window = me.getUnitWindow(me.createEmptyUnit()).show();

        window.setTitle('Nieuwe artikel eenheid');
        window.show();
    },

    onEditUnit: function (button) {
        var me = this,
            form = button.findParentByType('productform'),
            unit = form.fields.Unit.findRecord('Name', form.fields.Unit.getValue()),
            window = me.getUnitWindow(unit);

        window.show();
    },

    getUnitWindow: function (unit) {
        var me = this, window;

        window = me.createUnitWindow();
        if (!unit) {
            me.loadEmptyUnit(window);
        } else {
            window.setTitle('Bewerk eenheid: ' + unit.data.Name);
            window.loadWithUnit(unit);
        }
        return window;
    },

    createUnitWindow: function () {
        return Ext.create(this.getProductUnitWindowView());
    },

    addUnitToStore: function (unit) {
        var me = this,
            store = me.getUnitStore();

        store.add({UnitName: unit});
    },

    saveUnit: function (button) {
        var me = this,
            unit = me.getUnit(button);

        GenForm.server.UnitTest.SaveUnit(unit.data, {scope: me, callback:me.onUnitSaved});
    },

    onUnitSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('unitwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Unit saved: ', result.data.Name);
            me.addUnitToStore(result.data.Name);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Unit could not be saved: ', result.message);
        }
    },

    getUnitStore: function () {
        var me = this;
        return me.getProductUnitStore();
    },

    createEmptyUnit: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Unit');
    },

    loadEmptyUnit: function (window) {
        var me = this;
        window.loadWithUnit(me.createEmptyUnit());
    }

});
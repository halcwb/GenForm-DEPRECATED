Ext.define('GenForm.controller.mixin.UnitHandler', {

    getUnit: function (button) {
        return button.up('panel').down('form').getUnit();
    },

    editOrAddUnit: function () {
        var me = this;
        me.getUnitWindow().show();
    },

    getUnitWindow: function () {
        var me = this, form;

        form = me.createUnitWindow();
        me.loadEmptyUnit(form);
        return form;
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

        Product.AddNewUnit(unit.data, {scope: me, callback:me.onUnitSaved});
    },

    onUnitSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('unitwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Unit saved: ', result.data.UnitName);
            me.addUnitToStore(result.data.UnitName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Unit could not be saved: ', result.message);
        }
    },

    getUnitStore: function () {
        var me = this;
        return me.getProductUnitNameStore();
    },

    createEmptyUnit: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.UnitName');
    },

    loadEmptyUnit: function (window) {
        var me = this;
        window.loadWithUnit(me.createEmptyUnit());
    }

});
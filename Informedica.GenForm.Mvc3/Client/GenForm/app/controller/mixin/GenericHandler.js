Ext.define('GenForm.controller.mixin.GenericHandler', {

    addGenericToStore: function (generic) {
        var me = this,
            store = me.getGenericStore();

        store.add({GenericName: generic});
    },

    createEmptyGeneric: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.GenericName');
    },

    createGenericWindow: function () {
        return Ext.create(this.getProductGenericWindowView());
    },

    editOrAddGeneric: function () {
        var me = this;
        me.getGenericWindow().show();
    },

    getGeneric: function (button) {
        return button.up('panel').down('form').getGeneric();
    },

    getGenericWindow: function () {
        var me = this, form;

        form = me.createGenericWindow();
        me.loadEmptyGeneric(form);
        return form;
    },

    loadEmptyGeneric: function (window) {
        window.loadWithGeneric(this.createEmptyGeneric());
    },

    onGenericSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('genericwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Generic saved: ', result.data.GenericName);
            me.addGenericToStore(result.data.GenericName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Generic could not be saved: ', result.message);
        }
    },

    saveGeneric: function (button) {
        var me = this,
            generic = me.getGeneric(button);

        Product.AddNewGeneric(generic.data, {scope: me, callback:me.onGenericSaved});
    },

    getGenericStore: function () {
        var me = this;
        return me.getProductGenericNameStore();
    }

});
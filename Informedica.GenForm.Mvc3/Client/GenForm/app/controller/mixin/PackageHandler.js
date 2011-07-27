Ext.define('GenForm.controller.mixin.PackageHandler', {

    addPackageToStore: function (productPackage) {
        var me = this,
            store = me.getPackageStore();

        store.add({PackageName: productPackage});
    },

    createEmptyPackage: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.PackageName');
    },

    createPackageWindow: function () {
        return Ext.create(this.getProductPackageWindowView());
    },

    editOrAddPackage: function () {
        var me = this;
        me.getPackageWindow().show();
    },

    getPackage: function (button) {
        return button.up('panel').down('form').getPackage();
    },

    getPackageStore: function () {
        var me = this;
        return me.getProductPackageNameStore();
    },

    loadEmptyPackage: function (window) {
        window.loadWithPackage(this.createEmptyPackage());
    },

    onPackageSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('packagewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Package saved: ', result.data.PackageName);
            me.addPackageToStore(result.data.PackageName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Package could not be saved: ', result.message);
        }
    },

    savePackage: function (button) {
        var me = this,
            productPackage = me.getPackage(button);

        Product.AddNewPackage(productPackage.data, {scope: me, callback:me.onPackageSaved});
    },

    getPackageWindow: function () {
        var me = this, form;

        form = me.createPackageWindow();
        me.loadEmptyPackage(form);
        return form;
    }
});
Ext.define('GenForm.controller.mixin.PackageHandler', {

    addPackageToStore: function (productPackage) {
        var me = this,
            store = me.getPackageStore();

        store.add({PackageName: productPackage});
    },

    createEmptyPackage: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Package');
    },

    createPackageWindow: function () {
        return Ext.create(this.getProductPackageWindowView());
    },

    onAddPackage: function () {
        var me = this,
            window = me.getPackageWindow(me.createEmptyPackage()).show();

        window.setTitle('Nieuwe verpakking');
        window.show();
    },

    onEditPackage: function (button) {
        var me = this,
            form = button.findParentByType('productform'),
            pack = form.fields.Package.findRecord('Name', form.fields.Package.getValue()),
            window = me.getPackageWindow(pack);

        window.show();
    },

    getPackage: function (button) {
        return button.up('panel').down('form').getPackage();
    },

    getPackageStore: function () {
        var me = this;
        return me.getProductPackageStore();
    },

    loadEmptyPackage: function (window) {
        window.loadWithPackage(this.createEmptyPackage());
    },

    onPackageSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('packagewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Package saved: ', result.data.Name);
            me.addPackageToStore(result.data.Name);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Package could not be saved: ', result.message);
        }
    },

    onSavePackage: function (button) {
        var me = this,
            productPackage = me.getPackage(button);

        GenForm.server.UnitTest.SavePackage(productPackage.data, {scope: me, callback:me.onPackageSaved});
    },

    getPackageWindow: function (pack) {
        var me = this, window;

        window = me.createPackageWindow();
        if (!pack) {
            me.loadEmptyPackage(window);
        } else {
            window.setTitle('Bewerk verpakking: ' + pack.data.Name);
            window.loadWithPackage(pack);
        }

        return window;
    }
});
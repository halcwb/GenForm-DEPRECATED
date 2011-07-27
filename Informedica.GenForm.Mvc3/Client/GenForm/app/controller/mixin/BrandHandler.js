Ext.define('GenForm.controller.mixin.BrandHandler', {

    addBrandToStore: function (brand) {
        var me = this,
            store = me.getBrandStore();

        store.add({BrandName: brand});
    },

    createBrandWindow: function () {
        return Ext.create(this.getProductBrandWindowView());
    },

    createEmptyBrand: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.BrandName');
    },

    editOrAddBrand: function () {
        var me = this;
        me.showBrandWindow();
    },

    getBrand: function (button) {
        return button.up('panel').down('form').getBrand();
    },

    getBrandStore: function () {
        var me = this;
        return me.getProductBrandNameStore();
    },

    getBrandWindow: function () {
        var me = this, window;

        window = me.createBrandWindow();
        me.loadEmptyBrand(window);
        return window;
    },

    loadEmptyBrand: function (window) {
        window.loadWithBrand(this.createEmptyBrand());
    },

    onBrandSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('brandwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Brand saved:',  result.data.BrandName);
            me.addBrandToStore(result.data.BrandName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Brand could not be saved: ' + result.message);
        }
    },

    saveBrand: function (button) {
        var me = this,
            brand = me.getBrand(button);

        Product.AddNewBrand(brand.data, {scope: me, callback:me.onBrandSaved});
    },

    showBrandWindow: function () {
        var me = this;

        me.getBrandWindow().show();
    }
});
//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.controller.mixin.ProductHandler', {

    mixins: {
        genericHandler: 'GenForm.controller.mixin.GenericNameHandler',
        brandHandler: 'GenForm.controller.mixin.BrandNameHandler',
        shapeHandler: 'GenForm.controller.mixin.ShapeHandler',
        packageHandler: 'GenForm.controller.mixin.PackageHandler',
        unitHandler: 'GenForm.controller.mixin.UnitHandler',
        productSubstanceHandler: 'GenForm.controller.mixin.ProductSubstanceHandler',
        substanceUnitHandler: 'GenForm.controller.mixin.SubstanceUnitHandler'
    },

    saveProduct: function (button) {
        var me = this,
            product = me.getProduct(button);

        GenForm.server.UnitTest.SaveProduct(product.data, {scope: me, callback: me.onProductSaved});
    },

    loadEmptyProduct: function (window) {
        window.loadWithProduct(this.createEmptyProduct());
    },

    onProductSaved: function (result) {

        if (result.success) {
            Ext.MessageBox.alert('Product saved: ', result.data.DisplayName);

            // ToDo: ugly fix, write something better
            Ext.Array.each(Ext.ComponentQuery.query('productwindow'), function (item) {
                item.close();
                item.destroy();
            });
        } else {
            Ext.MessageBox.alert('Product could not be saved: ', result.message);
        }
    },

    getProduct: function (button) {
        var me = this;
        if (!button) throw new Error('['+ Ext.getDisplayName(me) +'] button undefined');
        if (!button.up('panel')) throw new Error('['+ Ext.getDisplayName(me) +'] panel undefined');
        if (!button.up('panel').down('form')) throw new Error('['+ Ext.getDisplayName(me) +'] form undefined');

        return button.up('panel').down('form').getProduct()
    },

    createEmptyProduct: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Product');
    },

    showProductWindow: function () {
        this.getProductWindow().show();
    },

    getProductWindow: function () {
        var me = this, window;

        window = me.createProductWindow();
        me.loadEmptyProduct(window);
        return window;
    },

    createProductWindow: function () {
        var me = this;
        return Ext.create(me.getProductProductWindowView());
    }

});
Ext.define('GenForm.test.usecase.ProductListTest', {

    describe: 'ProductListTests that',

    tests: function () {
        var me = this; //, queryHelper = Ext.create('GenForm.lib.util.QueryHelper');

        me.getProductList = function () {
            return Ext.ComponentQuery.query('panel[region=west] gridview')[0];
        };

        it('User can see a list view for products in the west region', function () {
            expect(me.getProductList()).toBeDefined();
        });

//        it('The list has an entry for the new product', function () {
//            expect(me.getProductList().store.data.length > 0).toBeTruthy();
//        });
    }
});
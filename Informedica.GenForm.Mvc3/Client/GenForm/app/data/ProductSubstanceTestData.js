/**
 * Created by .
 * User: hal
 * Date: 9-5-11
 * Time: 9:20
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.data.ProductSubstanceTestData', {
    extend: 'Ext.data.Store',
    storeId: 'productSubstanceTestStore',

    model: 'GenForm.model.product.ProductSubstance',

    data: {
        'items' : [
            { 'SortOrder': '1', 'Substance': 'paracetamol', 'Quantity': '500', 'Unit': 'mg' }
        ]
    },

    proxy: {
        type: 'memory',
        reader: {
            type: 'json',
            root: 'items'
        }
    }
});
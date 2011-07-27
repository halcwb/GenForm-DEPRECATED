Ext.define('GenForm.test.TestLoader', {
    
   loadTests: function (testList) {
        var test;
        for (var i = 0; i < testList.tests.length; i++) {
            test = Ext.create(testList.tests[i]);
            describe(test.describe, test.tests);
        }
   }
});